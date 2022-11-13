using UnityEngine;

namespace Game {
	public class Bottle : MonoBehaviour {
		#region Inspector fields
		public Alcohol alcohol;
		public ParticleSystem particle;
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
					transform.rotation = Quaternion.Euler(0, 0, BartendingManager.instance.bottleAngle);
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
			anchor = transform.position;
			Using = false;
			particle.startColor = alcohol.color;
			particle.trigger.AddCollider(BartendingManager.instance.shaker.enteringPlane);
		}

		void Update() {
			if(Using) {
				transform.position = BartendingManager.instance.PointingPosition;
			}
		}
		#endregion
	}
}