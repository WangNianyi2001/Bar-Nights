using UnityEngine;
using System.Collections;

namespace Game {
	[CreateAssetMenu(menuName = "Game/GameManager")]
	public class GameManagerSO : ScriptableObject {
		GameManager game => GameManager.instance;

		#region State management
		public void DeactivateAll() => game.DeactivateAll();
		public void SwitchToDialogue() => game.SwitchToDialogue();
		public void SwitchToDialogueNonExclusively() => game.SwitchToDialogueNonExclusively();
		public void SwitchToBartending() => game.SwitchToBartending();
		public void SwitchToCustomer() => game.SwitchToCustomer();
		public void SwitchToEntering() => game.SwitchToEntering();
		#endregion

		#region In-dialogue bartending
		public void StartBartendingFromDialogue() => game.StartBartendingFromDialogue();
		public void ServeBartendedAlchohol() => game.ServeBartendedAlchohol();
		#endregion

		#region Dialogue
		public void StartDialogue(string name) => game.StartDialogue(name);
		public void EndDialogue() => game.EndDialogue();
		public void SetCurrentCustomerAppearance(Sprite sprite) => game.SetCurrentCustomerAppearance(sprite);
		#endregion

		#region Customer
		public void SpawnMainCustomer(Customer data) => game.SpawnMainCustomer(data);
		public void SpawnSecondCustomer(Customer data) => game.SpawnSecondCustomer(data);
		public void ViewMainCustomer() => game.ViewMainCustomer();
		#endregion
	}
}
