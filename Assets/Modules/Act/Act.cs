using UnityEngine;
using System;
using System.Collections.Generic;

namespace Game {
	[CreateAssetMenu(menuName = "Act")]
	public class Act : ScriptableObject {
		public Customer openingCustomer;
		public string openingDialogue;
		public AudioClip bgm;

		[Serializable] public struct SpawnPair {
			public Customer customer;
			public string location;
		}
		public List<SpawnPair> spawnPairs;
	}
}