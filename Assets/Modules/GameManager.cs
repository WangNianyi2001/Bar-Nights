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
		Status _status;
		public Status status {
			get => _status;
			set => SwitchTo(value);
		}
		[NonSerialized] public ViewManager view;
		[NonSerialized] public BartendingManager bartending;
		[NonSerialized] public DialogueSystemController dialogue;
		[NonSerialized] public UIManager ui;
		#endregion

		#region Public interfaces
		public void DisableAll() {
			view.Deactivate();
			ui.Deactivate();
			bartending.Active = false;
		}

		public void SwitchToDialogue() {
			if(status == Status.Bartending)
				bartending.Active = false;
			view.SwitchTo(view.dialogue);
			ui.SwitchTo(ui.dialogue);
		}

		public void SwitchToBartending() {
			view.SwitchTo(view.bartending);
			ui.SwitchTo(ui.bartending);
			bartending.Active = true;
		}

		public void SwitchTo(Status status) {
			if(_status == status)
				return;
			switch(status) {
				case Status.None:
					DisableAll();
					break;
				case Status.Dialogue:
					SwitchToDialogue();
					break;
				case Status.Bartending:
					SwitchToBartending();
					break;
			}
			_status = status;
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
			status = start;
		}
		#endregion
	}
}