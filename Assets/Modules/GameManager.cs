using UnityEngine;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections;

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
		public enum Status { None, Dialogue, Bartending, Customer, Entering }
		public Status start = Status.None;
		#endregion

		#region Core 
		Status status;
		[NonSerialized] public ViewManager view;
		[NonSerialized] public BartendingManager bartending;
		[NonSerialized] public DialogueSystemController dialogue;
		[NonSerialized] public UIManager ui;
		#endregion

		#region Public interfaces
		public void SwitchTo(Status value) {
			if(status == value)
				return;
			bartending.Active = false;
			switch(value) {
				case Status.None:
					view.Deactivate();
					ui.Deactivate();
					break;
				case Status.Dialogue:
					view.SwitchTo(view.dialogue);
					ui.SwitchTo(ui.dialogue);
					break;
				case Status.Bartending:
					view.SwitchTo(view.bartending);
					ui.SwitchTo(ui.bartending);
					bartending.Active = true;
					break;
			}
			status = value;
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
			SwitchTo(start);
		}
		#endregion
	}
}