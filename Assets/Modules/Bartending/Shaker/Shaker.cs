using UnityEngine;

public class Shaker : MonoBehaviour {
	#region Singleton
	public static Shaker instance;
	public Shaker() {
		instance = this;
	}
	#endregion

	#region Inspector Fields
	public Collider enteringPlane;
	#endregion

	#region Core Fields
	public float liquidAmount = 0;
	#endregion

	public void OnReceivingLiquid(Bottle bottle) {
		liquidAmount += 0.1f;
	}
}
