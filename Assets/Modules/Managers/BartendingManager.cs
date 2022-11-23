using UnityEngine;

namespace Game {
	public class BartendingManager : MonoBehaviour {
		#region Inspector fields
		public Shaker shaker;
		public Collider bartendingPlane;
		public RectTransform mixPivot;
		public RectTransform mixTarget;

		public float bottleAngle;
		#endregion

		#region Core fields
		Vector3 lastPointingPosition = Vector3.zero;
		#endregion

		#region Auxiliary
		float Rand() => Random.value * 2 - 1;
		#endregion

		#region Public interfaces
		public Vector3 PointingPosition {
			get {
				var ray = GameManager.instance.camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				bartendingPlane.Raycast(ray, out hit, 10);
				if(hit.collider == null)
					return lastPointingPosition;
				return lastPointingPosition = hit.point;
			}
		}

		public bool Active {
			set {
				foreach(var bottle in Bottle.all)
					bottle.usable.enabled = value;
				shaker.usable.enabled = value;
			}
		}

		public void StartBartending() {
			Vector2 target = new Vector2(Rand(), Rand()).normalized;
			if(target.magnitude < .2f)
				target /= .5f;
			target /= 1.5f;
			mixTarget.anchoredPosition = target * (mixTarget.parent as RectTransform).sizeDelta / 2;
			shaker.Mix = Vector2.zero;
		}

		public bool Reached => (mixTarget.anchoredPosition - mixPivot.anchoredPosition).magnitude < mixTarget.sizeDelta.magnitude / 2;
		#endregion
	}
}