using UnityEngine;

namespace Game {
	[CreateAssetMenu(menuName = "Game/Alcohol")]
	public class Alcohol : ScriptableObject {
		new public string name;
		public Color color = Color.white;
		public Vector2 vector;
	}
}