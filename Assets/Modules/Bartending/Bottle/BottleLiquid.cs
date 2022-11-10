using UnityEngine;

public class BottleLiquid : MonoBehaviour {
	public Bottle bottle;

	void OnParticleTrigger() {
		Shaker.instance.OnReceivingLiquid(bottle);
	}
}
