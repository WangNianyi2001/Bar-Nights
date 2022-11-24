using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class ViewManager : MonoBehaviour {
	public CinemachineVirtualCamera entering, customerArea, dialogue, bartending;
	public IEnumerable<CinemachineVirtualCamera> views =>
		new CinemachineVirtualCamera[] { entering, customerArea, dialogue, bartending };

	public void Deactivate() {
		foreach(var view in views) {
			if(view)
				view.enabled = false;
		}
	}
	public void SwitchToNonExclusively(CinemachineVirtualCamera view) {
		if(view)
			view.enabled = true;
	}
	public void SwitchTo(CinemachineVirtualCamera view) {
		Deactivate();
		SwitchToNonExclusively(view);
	}
}
