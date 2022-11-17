using UnityEngine;

namespace Game {
	public class BartendingManager : MonoBehaviour {
		#region Inspector fields
		public Shaker shaker;
		public Collider bartendingPlane;
		public RectTransform mixPivot;

		public float bottleAngle;
		#endregion

		#region
		Vector3 lastPointingPosition = Vector3.zero;
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
		#endregion
	}
}