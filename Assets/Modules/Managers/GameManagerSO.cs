using UnityEngine;

namespace Game {
	[CreateAssetMenu(menuName = "Game/GameManager")]
	public class GameManagerSO : ScriptableObject {
		GameManager game => GameManager.instance;

		#region In-dialogue bartending
		public void StartBartendingFromDialogue() => game.StartBartendingFromDialogue();
		public void ServeBartendedAlchohol() => game.ServeBartendedAlchohol();
		#endregion

		#region Dialogue management
		public void StartDialogue(string name) => game.StartDialogue(name);
		public void EndDialogue() => game.EndDialogue();
		public void SetCurrentCustomerAppearance(Sprite sprite) => game.SetCurrentCustomerAppearance(sprite);
		#endregion
	}
}
