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
		new public Camera camera;
		public InputActionAsset actions;
		#endregion

		#region Core fields
		[NonSerialized] public ViewManager view;
		[NonSerialized] public BartendingManager bartending;
		[NonSerialized] public DialogueSystemController dialogue;
		[NonSerialized] public UIManager ui;
		CustomerBehaviour currentCustomer;
		#endregion

		#region Public interfaces
		#region State management
		public void DeactivateAll() {
			bartending.Active = false;
			CustomerBehaviour.AllEnabled = false;
			ui.Deactivate();
			view.Deactivate();
		}
		public void SwitchToDialogue() {
			DeactivateAll();
			view.SwitchTo(view.dialogue);
			ui.SwitchTo(ui.dialogue);
			CustomerBehaviour.AllEnabled = true;
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

		#region Dialogue management
		public CustomerBehaviour CurrentCustomer {
			get => currentCustomer;
			set {
				currentCustomer = value;
			}
		}
		public void StartDialogue(string name) {
			dialogue.StopAllConversations();
			SwitchToDialogue();
			dialogue.StartConversation(name);
		}
		public void EndDialogue() {
			dialogue.StopAllConversations();
			CurrentCustomer = null;
			DialogueLua.SetVariable("Bartending Count", 0);
			DialogueLua.SetVariable("Current Dialogue", "");
		}
		public void SetCurrentCustomerAppearance(Sprite sprite) {
			CurrentCustomer?.SetAppearance(sprite);
		}
		#endregion
		#endregion

		#region Life cycle
		void Awake() {
			view = GetComponent<ViewManager>();
			bartending = GetComponent<BartendingManager>();
			dialogue = GetComponent<DialogueSystemController>();
			ui = GetComponent<UIManager>();
		}

		void Start() {
			InputDeviceManager.RegisterInputAction("Use", actions.FindAction("Use"));
			DeactivateAll();
		}
		#endregion
	}
}