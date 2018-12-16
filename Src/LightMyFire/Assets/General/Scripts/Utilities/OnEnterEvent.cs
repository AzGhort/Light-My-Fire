using UnityEngine.Events;
using UnityEngine;

namespace LightMyFire
{
	public class OnEnterEvent : MonoBehaviour
	{
		[SerializeField] private UnityEvent uEvent;
		[SerializeField] private bool repeatableEvent = false;

		private void OnTriggerEnter2D(Collider2D collision) {
			if (collision.gameObject.CompareTag("Player")) {
				uEvent.Invoke();
				if (!repeatableEvent) { Destroy(gameObject); }
			}
		}
	}
}