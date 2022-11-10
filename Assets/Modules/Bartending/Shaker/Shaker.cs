using UnityEngine;

public class Shaker : MonoBehaviour {
	#region Inspector fields
	public Collider enteringPlane;
	public float volume;
	#endregion

	#region Core fields
	public float liquidAmount = 0;
	#endregion

	public void OnReceivingLiquid(Bottle bottle, int exittingParticleCount) {
		liquidAmount += exittingParticleCount * 0.1f;
	}
}
