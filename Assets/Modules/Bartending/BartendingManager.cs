using UnityEngine;

namespace Game {
	public class BartendingManager : MonoBehaviour {
		#region Singleton
		public static BartendingManager instance;
		public BartendingManager() {
			instance = this;
		}
		#endregion

		#region Inspector fields
		public Shaker shaker;
		new public Camera camera;
		public Collider bartendingPlane;
		public RectTransform mixPivot;
		public float liquidReceivingRate;

		public float bottleAngle;
		#endregion

		#region
		Vector3 lastPointingPosition = Vector3.zero;
		#endregion

		#region Public interfaces
		public Vector3 PointingPosition {
			get {
				var ray = camera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				bartendingPlane.Raycast(ray, out hit, 10);
				if(hit.collider == null)
					return lastPointingPosition;
				return lastPointingPosition = hit.point;
			}
		}
		#endregion
	}
}