using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;

namespace Game {
	public class Customer : MonoBehaviour {
		#region Static
		public static List<Customer> all = new List<Customer>();
		static bool allEnabled = true;
		public static bool AllEnabled {
			get => allEnabled;
			set {
				allEnabled = value;
				foreach(var c in all)
					c.usable.enabled = allEnabled;
			}
		}
		#endregion

		#region Inspector fields
		public Usable usable;
		public List<string> dialogues = new List<string>();
		#endregion

		#region Core fields
		IEnumerator<string> nextDialogue;
		#endregion

		#region Public interfaces
		public bool Enabled {
			get => usable.enabled;
			set => usable.enabled = value;
		}

		public void StartNextDialogue() {
			if(!nextDialogue.MoveNext())
				return;
			GameManager.instance.StartDialogue(nextDialogue.Current);
		}
		#endregion

		#region Life cycle
		void Start() {
			all.Add(this);
			Enabled = AllEnabled;
			nextDialogue = dialogues.GetEnumerator();
		}

		void OnDestroy() {
			all.Remove(this);
		}
		#endregion
	}
}