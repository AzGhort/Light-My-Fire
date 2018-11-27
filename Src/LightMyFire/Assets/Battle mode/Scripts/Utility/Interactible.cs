using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace TentativeMaster
{
	public class Interactible : MonoBehaviour
	{
		[SerializeField] private UnityEvent uEvent;

		private void OnTriggerEnter2D(Collider2D collision) {
			if (collision.gameObject.CompareTag("Player")) {
				collision.gameObject.GetComponent<PlayerInteractor>().RegisterCollider(uEvent);
			}
		}

		private void OnTriggerExit2D(Collider2D collision) {
			if (collision.gameObject.CompareTag("Player")) {
				collision.gameObject.GetComponent<PlayerInteractor>().DeleteCollider(uEvent);
			}
		}
	}
}