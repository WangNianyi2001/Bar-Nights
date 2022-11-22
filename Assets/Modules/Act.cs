using UnityEngine;
using System.Collections.Generic;

namespace Game {
	[CreateAssetMenu(menuName = "Act")]
	public class Act : ScriptableObject {
		public Customer openingCustomer;
		public List<Customer> customersInSeat;
	}
}