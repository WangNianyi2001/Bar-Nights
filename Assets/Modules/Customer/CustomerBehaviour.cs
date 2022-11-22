using UnityEngine;
using UnityEngine.AI;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;

namespace Game {
	public class CustomerBehaviour : MonoBehaviour {
		#region Inspector fields
		public SpriteRenderer spriteRenderer;
		public Usable usable;
		public Transform head;
		public NavMeshAgent agent;
		#endregion

		#region Core fields
		IEnumerator<string> nextDialogue;
		Customer customer;
		#endregion

		#region Auxiliary
		IEnumerator StartDialogueWhenArrivingBar() {
			SetAsCurrent();
			yield return new WaitForSeconds(.5f);
			yield return new WaitUntil(() => agent.remainingDistance < .5f);
			StartNextDialogue();
		}
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
		public void SetAsCurrent() => GameManager.instance.customer.Current = this;

		public void GoTo(Transform destination) => agent.destination = destination.position;
		public void GoToBar() {
			GoTo(GameManager.instance.anchors.bar);
			StartCoroutine(StartDialogueWhenArrivingBar());
		}
		#endregion
	}
}