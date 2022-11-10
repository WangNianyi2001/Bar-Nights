using UnityEngine;
using System.Collections.Generic;

public class BottleLiquid : MonoBehaviour {
	public Bottle bottle;

	void OnParticleTrigger() {
		var particles = new List<ParticleSystem.Particle>();
		bottle.particle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
		BartendingManager.instance.shaker.OnReceivingLiquid(bottle, particles.Count);
	}
}
