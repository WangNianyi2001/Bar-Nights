using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;

namespace Game {
#pragma warning disable CS0618
	public class Bottle : MonoBehaviour {
		#region Static
		public static List<Bottle> all = new List<Bottle>();
		#endregion

		#region Inspector fields
		public Alcohol alcohol;
		public ParticleSystem particle;
		public Usable usable;
		#endregion

		#region Core fields
		Vector3 anchor;
		#endregion

		#region Public interfaces
		public bool Using {
			get => particle.emission.enabled;
			set {
				particle.enableEmission = value;
				if(value) {
					transform.rotation = Quaternion.Euler(0, 0, GameManager.instance.bartending.bottleAngle);
				}
				else {
					transform.position = anchor;
					transform.rotation = Quaternion.identity;
				}
			}
		}

		public void Interact() {
			Using = !Using;
		}
		#endregion

		#region Life cycle
		void Start() {
			all.Add(this);

			anchor = transform.position;
			Using = false;
			particle.startColor = alcohol.color;
			particle.trigger.AddCollider(GameManager.instance.bartending.shaker.enteringPlane);
			usable.overrideName = alcohol.name;
		}

		void Update() {
			if(Using) {
				transform.position = GameManager.instance.bartending.PointingPosition;
			}
		}

		void OnDestroy() {
			all.Remove(this);
		}
		#endregion
	}
#pragma warning restore CS0618
}