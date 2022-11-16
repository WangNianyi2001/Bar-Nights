using UnityEngine;
using System.Collections.Generic;

namespace Game {
	public class UIManager : MonoBehaviour {
		public RectTransform dialogue, bartending;
		public IEnumerable<RectTransform> views =>
			new RectTransform[] { dialogue, bartending };

		public void Deactivate() {
			foreach(var view in views)
				view?.gameObject?.SetActive(false);
		}
		public void SwitchTo(RectTransform view) {
			Deactivate();
			view?.gameObject?.SetActive(true);
		}
	}
}