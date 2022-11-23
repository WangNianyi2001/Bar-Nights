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
		public CustomerBehaviour SpawnCustomerAt(Customer customer, Transform location) {
			if(!location)
				return null;
			GameObject go = Instantiate(customerPrefab);
			go.transform.position = location.position;
			CustomerBehaviour cb = go.GetComponentInChildren<CustomerBehaviour>(true);
			all.Add(cb);
			cb.Customer = customer;
			go.SetActive(true);
			return cb;
		}
		public CustomerBehaviour SpawnCustomer(Customer customer) => SpawnCustomerAt(customer, GameManager.instance.anchors.entrance);
		#endregion

		#region Public interfaces
		public CustomerBehaviour Current {
			get => current;
			set {
				current = value;
				GameManager.instance.view.entering.LookAt = current?.head;
			}
		}

		public void SpawnMainCustomer(Customer data) {
			var customer = SpawnCustomer(data);
			customer.GoToBar();
			current = customer;
			GameManager.instance.SwitchToEntering();
		}

		public CustomerBehaviour Find(string name) {
			return all.Find(x => x.Customer.name == name);
		}
		#endregion
	}
}