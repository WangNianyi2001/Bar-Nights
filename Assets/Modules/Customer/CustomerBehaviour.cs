using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using System.Collections;

namespace Game {
	public class CustomerBehaviour : MonoBehaviour {
		#region Inspector fields
		public SpriteRenderer spriteRenderer;
		public Usable usable;
		public Animator animator;
		public Transform headPos;
		#endregion

		#region Core fields
		IEnumerator<string> nextDialogue;
		Customer customer;
		#endregion

		#region Public interfaces
		public bool Enabled {
			get => usable.enabled;
			set => usable.enabled = value;
		}

		public Customer Customer {
			get => customer;
			set {
				customer = value;
				nextDialogue = customer.dialogues.GetEnumerator();
				SetAppearance(customer.sprite);
			}
		}
		public void StartNextDialogue() {
			if(!nextDialogue.MoveNext())
				return;
			GameManager game = GameManager.instance;
			game.customer.Current = this;
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

		#region Animation
		public void PlayAnimation(string name) => animator.Play(name, -1, 0);
		[ContextMenu("EnterToBar")]
		public void EnterToBar() {
			PlayAnimation("EnterToBar");
			GameManager.instance.customer.Current = this;
		}
		#endregion
		#endregion

		#region Life cycle
		void Start() {
			//
		}
		#endregion
	}
}