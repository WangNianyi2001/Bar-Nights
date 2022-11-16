using UnityEngine;

namespace Game {
	public class GameManager : MonoBehaviour {
		#region Singleton
		public static GameManager instance;
		public GameManager() {
			instance = this;
		}
		#endregion

		public ViewManager view;
		public BartendingManager bartending;
	}
}