using UnityEngine;
using System.Collections.Generic;

namespace Game {
	public class BottleLiquid : MonoBehaviour {
		public Bottle bottle;

		void OnParticleTrigger() {
			var particles = new List<ParticleSystem.Particle>();
			bottle.particle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
			if(particles.Count > 0)
				GameManager.instance.bartending.shaker.ReceiveLiquid(bottle);
		}
	}
}