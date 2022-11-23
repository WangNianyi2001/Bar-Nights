using UnityEngine;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Game {
	public class GameManager : MonoBehaviour {
		#region Singleton
		public static GameManager instance;
		public GameManager() {
			instance = this;
		}
		#endregion

		#region Inspector fields
		public Act act;
		new public Camera camera;
		public InputActionAsset actions;
		#endregion

		#region Core fields
		[NonSerialized] public ViewManager view;
		[NonSerialized] public BartendingManager bartending;
		[NonSerialized] public DialogueSystemController dialogue;
		[NonSerialized] public UIManager ui;
		[NonSerialized] public CustomerManager customer;
		[NonSerialized] public AnchorManager anchors;
		#endregion

		#region Auxiliary
		#region State management
		public void DeactivateAll() {
			bartending.Active = false;
			customer.AllEnabled = false;
			ui.Deactivate();
			view.Deactivate();
		}
		public void SwitchToDialogue() {
			DeactivateAll();
			view.SwitchTo(view.dialogue);
			ui.SwitchTo(ui.dialogue);
			customer.AllEnabled = true;
		}
		public void SwitchToDialogueNonExclusively() => view.SwitchTo(view.dialogue);
		public void SwitchToBartending() {
			DeactivateAll();
			view.SwitchTo(view.bartending);
			ui.SwitchTo(ui.bartending);
			bartending.Active = true;
		}
		public void SwitchToCustomer() {
			DeactivateAll();
			view.SwitchTo(view.customerArea);
		}
		public void SwitchToEntering() {
			DeactivateAll();
			view.SwitchTo(view.entering);
		}
		#endregion
		#endregion

		#region Public interfaces
		#region In-dialogue bartending
		public void StartBartendingFromDialogue() {
			dialogue.StopAllConversations();
			bartending.StartBartending();
			SwitchToBartending();
		}
		public void ServeBartendedAlchohol() {
			if(!bartending.Reached) {
				dialogue.ShowAlert("这不是顾客想要的酒", 2);
				return;
			}

			SwitchToDialogue();

			int count = DialogueLua.GetVariable("Bartending Count").asInt;
			DialogueLua.SetVariable("Bartending Count", count + 1);

			string name = DialogueLua.GetVariable("Current Dialogue").asString;
			dialogue.StartConversation(name);
		}
		#endregion

		#region Dialogue
		public void StartDialogue(string name) {
			dialogue.StopAllConversations();
			SwitchToDialogue();
			dialogue.StartConversation(name);
		}
		public void EndDialogue() {
			dialogue.StopAllConversations();
			customer.Current = null;
			DialogueLua.SetVariable("Bartending Count", 0);
			DialogueLua.SetVariable("Current Dialogue", "");
		}
		public void SetCurrentCustomerAppearance(Sprite sprite) {
			customer.Current?.SetAppearance(sprite);
		}
		#endregion

		#region Customer
		public void SpawnMainCustomer(Customer data) => customer.SpawnMainCustomer(data);
		public void SpawnSecondCustomer(Customer data) {
			var cb = customer.SpawnCustomer(data);
			cb.GoTo(anchors.secondBar);
			view.entering.LookAt = cb.head;
			view.SwitchTo(view.entering);
		}
		public void ViewMainCustomer() => view.entering.LookAt = customer.Current.head;
		public void GoToBarAsSecond(string customerName) {
			customer.Find(customerName)?.GoTo(anchors.secondBar);
		}
		public void LeaveBar(string customerName) {
			customer.Find(customerName)?.GoTo(anchors.entrance);
		}
		#endregion

		#region Act
		public void StartAct() {
			customer.SpawnMainCustomer(act.openingCustomer);
		}

		public void EndAct() {
			ui.SwitchTo(ui.end);
			view.SwitchTo(view.dialogue);
		}

		public void QuitAct() {
			SceneManager.LoadScene(ActLoader.loaderName);
		}
		#endregion
		#endregion

		#region Life cycle
		void Awake() {
			view = GetComponent<ViewManager>();
			bartending = GetComponent<BartendingManager>();
			dialogue = GetComponent<DialogueSystemController>();
			ui = GetComponent<UIManager>();
			customer = GetComponent<CustomerManager>();
			anchors = GetComponent<AnchorManager>();

			act = ActLoader.actToLoad ?? act;
		}

		void Start() {
			InputDeviceManager.RegisterInputAction("Use", actions.FindAction("Use"));
			DeactivateAll();
			foreach(var pair in act.spawnPairs)
				customer.SpawnCustomerAt(pair.customer, GameObject.Find(pair.location)?.transform);
			view.SwitchTo(view.customerArea);
			ui.SwitchTo(ui.start);
		}
		#endregion
	}
}