using UnityEngine;

namespace Game {
	[CreateAssetMenu(menuName = "Game/GameManager")]
	public class GameManagerSO : ScriptableObject {
		GameManager game => GameManager.instance;

		public void DeactivateAll() => game.DeactivateAll();
		public void SwitchToDialogue() => game.SwitchToDialogue();
		public void SwitchToBartending() => game.SwitchToBartending();
		public void SwitchToCustomer() => game.SwitchToCustomer();
		public void SwitchToEntering() => game.SwitchToEntering();

		public void StartBartendingFromDialogue() => game.StartBartendingFromDialogue();
		public void ServeBartendedAlchohol() => game.ServeBartendedAlchohol();
		public void StartDialogue(string name) => game.StartDialogue(name);
		public void EndDialogue() => game.EndDialogue();
	}
}
