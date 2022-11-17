using UnityEngine;
using PixelCrushers.DialogueSystem;

namespace Game {
	public class Shaker : MonoBehaviour {
		#region Inspector fields
		public Usable usable;
		public Collider enteringPlane;
		public float liquidReceivingRate;
		#endregion

		#region Core fields
		Vector2 mix;
		#endregion

		#region Public interfaces
		public Vector2 Mix {
			get => mix;
			set {
				mix = value;
				if(mix.magnitude > 1)
					mix = mix.normalized;
				RectTransform mixPivot = GameManager.instance.bartending.mixPivot;
				mixPivot.anchoredPosition = mix * (mixPivot.parent as RectTransform).sizeDelta / 2;
			}
		}

		public void ReceiveLiquid(Bottle bottle) {
			Mix += bottle.alcohol.vector * liquidReceivingRate;
		}

		public void Serve() {
			GameManager.instance.SwitchToDialogue();

			int bartenderCount = DialogueLua.GetVariable("Bartender Count").asInt;
			DialogueLua.SetVariable("Bartending Count", bartenderCount + 1);

			DialogueSystemController dialogue = GameManager.instance.dialogue;
			dialogue.StopAllConversations();
			string conversationName = DialogueLua.GetVariable("Current Conversation").asString;
			dialogue.StartConversation(conversationName);
		}
		#endregion
	}
}