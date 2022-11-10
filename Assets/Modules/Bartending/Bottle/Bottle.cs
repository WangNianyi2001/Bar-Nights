using UnityEngine;

public class Bottle : MonoBehaviour {
	public ParticleSystem particle;

	#region Public interfaces

#pragma warning disable CS0618
	public bool Pouring {
		get => particle.enableEmission;
		set => particle.enableEmission = value;
	}
#pragma warning restore CS0618
	#endregion

	void Start() {
		Pouring = false;
		particle.trigger.AddCollider(Shaker.instance.enteringPlane);
	}
}
