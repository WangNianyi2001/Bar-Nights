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

		public enum State { None, Dialogue, Bartending, Customer, Entering }

		#region Inspector fields
		new public Camera camera;
		public State start = State.None;
		public InputActionAsset actions;
		#endregion

		#region Core 
		State state;
		[NonSerialized] public ViewManager view;
		[NonSerialized] public BartendingManager bartending;
		[NonSerialized] public DialogueSystemController dialogue;
		[NonSerialized] public UIManager ui;
		#endregion

		#region Public interfaces
		public void DeactivateAll() => SwitchTo(State.None);
		public void SwitchToDialogue() => SwitchTo(State.Dialogue);
		public void SwitchToBartending() => SwitchTo(State.Bartending);
		public void SwitchToCustomer() => SwitchTo(State.Customer);
		public void SwitchToEntering() => SwitchTo(State.Entering);

		public void SwitchTo(State value) {
			if(state == value)
				return;
			ui.Deactivate();
			bartending.Active = false;
			switch(value) {
				case State.None:
					view.Deactivate();
					break;
				case State.Dialogue:
					view.SwitchTo(view.dialogue);
					ui.SwitchTo(ui.dialogue);
					break;
				case State.Bartending:
					view.SwitchTo(view.bartending);
					ui.SwitchTo(ui.bartending);
					bartending.Active = true;
					break;
				case State.Customer:
					view.SwitchTo(view.customerArea);
					break;
				case State.Entering:
					view.SwitchTo(view.entering);
					break;
			}
			state = value;
		}
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
			SwitchTo(start);
		}
		#endregion
	}
}