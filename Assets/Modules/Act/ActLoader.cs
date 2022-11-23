using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {
	[CreateAssetMenu(menuName = "Game/Act Loader")]
	public class ActLoader : ScriptableObject {
		public const string mainSceneName = "Main";
		public const string loaderName = "Level";

		public static Act actToLoad;

		public void LoadAct(Act act) {
			actToLoad = act;
			SceneManager.LoadScene(mainSceneName);
		}
	}
}