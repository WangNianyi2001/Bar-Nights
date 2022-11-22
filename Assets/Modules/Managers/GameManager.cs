using UnityEngine;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using UnityEngine.InputSystem;
using System;

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
			SwitchToBartending();
		}
		public void ServeBartendedAlchohol() {
			SwitchToDialogue();

			int bartenderCount = DialogueLua.GetVariable("Bartender Count").asInt;
			DialogueLua.SetVariable("Bartending Count", bartenderCount + 1);

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
		public void SpawnCustomerEnteringToBar(Customer data) => customer.SpawnCustomerEnteringToBar(data);
		#endregion
		#endregion

		#region Life cycle
		void Awake() {
			view = GetComponent<ViewManager>();
			bartending = GetComponent<BartendingManager>();
			dialogue = GetComponent<DialogueSystemController>();
			ui = GetComponent<UIManager>();
			customer = GetComponent<CustomerManager>();
		}

		void Start() {
			InputDeviceManager.RegisterInputAction("Use", actions.FindAction("Use"));
			DeactivateAll();
			customer.SpawnCustomerEnteringToBar(act.openingCustomer);
		}
		#endregion
	}
}