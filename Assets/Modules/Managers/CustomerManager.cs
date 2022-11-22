using UnityEngine;
using System;
using System.Collections.Generic;

namespace Game {
	public class CustomerManager : MonoBehaviour {
		#region Inspector fields
		public GameObject customerPrefab;
		#endregion

		#region Core fields
		[NonSerialized] public List<CustomerBehaviour> all = new List<CustomerBehaviour>();
		public bool AllEnabled {
			set {
				foreach(var c in all)
					c.usable.enabled = value;
			}
		}

		CustomerBehaviour current;
		#endregion

		#region Auxiliary
		public CustomerBehaviour SpawnCustomer(Customer customer) {
			GameObject go = Instantiate(customerPrefab, GameManager.instance.anchors.entrance);
			CustomerBehaviour cb = go.GetComponentInChildren<CustomerBehaviour>(true);
			all.Add(cb);
			cb.Customer = customer;
			go.SetActive(true);
			return cb;
		}
		#endregion

		#region Public interfaces
		public CustomerBehaviour Current {
			get => current;
			set {
				current = value;
				GameManager.instance.view.entering.LookAt = current.head;
			}
		}

		public void SpawnCustomerEnteringToBar(Customer data) {
			var customer = SpawnCustomer(data);
			customer.GoToBar();
			current = customer;
			GameManager.instance.SwitchToEntering();
		}
		#endregion
	}
}