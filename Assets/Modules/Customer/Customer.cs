using UnityEngine;

namespace Game {
	[CreateAssetMenu(menuName = "Game/Customer")]
	public class Customer : ScriptableObject {
		public new string name;
		public Sprite sprite;
	}
}