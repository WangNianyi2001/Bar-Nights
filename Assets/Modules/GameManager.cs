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
		public enum State { None, Dialogue, Bartending, Customer, Entering }
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
		public void SwitchTo(State value) {
			if(state == value)
				return;
			bartending.Active = false;
			switch(value) {
				case State.None:
					view.Deactivate();
					ui.Deactivate();
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