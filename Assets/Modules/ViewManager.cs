using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour {
	public CinemachineVirtualCamera entering, customerArea, dialogue, bartending;
	public IEnumerable<CinemachineVirtualCamera> views =>
		new CinemachineVirtualCamera[] { entering, customerArea, dialogue, bartending };

	public void Deactivate() {
		foreach(var view in views)
			view.enabled = false;
	}
	public void SwitchTo(CinemachineVirtualCamera view) {
		Deactivate();
		view.enabled = true;
	}

	void Start() {
		Deactivate();
	}
}
