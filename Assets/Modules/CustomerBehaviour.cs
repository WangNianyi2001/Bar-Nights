using UnityEngine;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;

namespace Game {
	[ExecuteInEditMode]
	public class CustomerBehaviour : MonoBehaviour {
		#region Static
		[NonSerialized] public static List<CustomerBehaviour> all = new List<CustomerBehaviour>();
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
		public Customer customer;
		public SpriteRenderer spriteRenderer;
		public Usable usable;
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
			GameManager game = GameManager.instance;
			game.CurrentCustomer = this;
			game.StartDialogue(nextDialogue.Current);
		}

		public void SetAppearance(Sprite sprite) {
			spriteRenderer.sprite = sprite;
			BoxCollider collider = spriteRenderer.GetComponent<BoxCollider>();
			if(collider) {
				collider.center = spriteRenderer.localBounds.center;
				collider.size = spriteRenderer.localBounds.size;
			}
		}
		#endregion

		#region Life cycle
		void Start() {
			if(!Application.isPlaying)
				return;
			all.Add(this);
			Enabled = AllEnabled;
			nextDialogue = customer.dialogues.GetEnumerator();
			SetAppearance(customer.sprite);
		}

		void Update() {
			if(!Application.isPlaying) {
				SetAppearance(customer?.sprite);
				return;
			}
		}

		void OnDestroy() {
			if(!Application.isPlaying)
				return;
			all.Remove(this);
		}
		#endregion
	}
}