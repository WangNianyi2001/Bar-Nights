using UnityEngine;
using System.Collections.Generic;

namespace Game {
	public class UIManager : MonoBehaviour {
		public Canvas uiRoot;
		public RectTransform dialogue, bartending;
		public IEnumerable<RectTransform> views =>
			new RectTransform[] { dialogue, bartending };

		public void Deactivate() {
			foreach(var view in views)
				view?.gameObject?.SetActive(false);
		}
		public void SwitchToNonExclusively(RectTransform view) {
			view?.gameObject?.SetActive(true);
		}
		public void SwitchTo(RectTransform view) {
			Deactivate();
			SwitchToNonExclusively(view);
		}

		void Awake() {
			uiRoot.gameObject.SetActive(true);
		}
	}
}