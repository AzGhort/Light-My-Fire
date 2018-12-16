using System.Collections;

using UnityEngine.Events;
using UnityEngine;

namespace LightMyFire
{
	public class ColliderEvent : MonoBehaviour
	{
		[SerializeField] private UnityEvent onEnterEvent = null;
		[SerializeField] private UnityEvent onLeaveEvent = null;
		[SerializeField] private bool repeatableEnter = false;
		[SerializeField] private bool repeatableLeave = false;
		[SerializeField] private float repeatEnterDelay = 0f;
		[SerializeField] private float repeatLeaveDelay = 0f;

		private bool entered = false;
		private bool left = false;
		private bool enterDelay = false;
		private bool leaveDelay = false;

		private void OnTriggerEnter2D(Collider2D collision) {
			if (onEnterEvent == null) { return; }
			if (enterDelay) { return; }
			if (collision.gameObject.CompareTag("Player")) {
				if (repeatableEnter || !entered) {
					entered = true;
					onEnterEvent.Invoke();

					StartCoroutine("enterDelayWait");
				}
			}
		}

		private void OnTriggerExit2D(Collider2D collision) {
			if (onLeaveEvent == null) { return; }
			if (leaveDelay) { return; }
			if (collision.gameObject.CompareTag("Player")) {
				if (repeatableLeave || !left) {
					left = true;
					onLeaveEvent.Invoke();

					StartCoroutine("leaveDelayWait");
				}
			}
		}

		private IEnumerator enterDelayWait() {
			enterDelay = true;
			yield return new WaitForSeconds(repeatEnterDelay);
			enterDelay = false;
		}

		private IEnumerator leaveDelayWait() {
			leaveDelay = true;
			yield return new WaitForSeconds(repeatLeaveDelay);
			leaveDelay = false;
		}
	}
}