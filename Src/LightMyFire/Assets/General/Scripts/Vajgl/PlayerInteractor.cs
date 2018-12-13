using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace LightMyFire
{
	public class PlayerInteractor : MonoBehaviour
	{
		private List<UnityEvent> events = new List<UnityEvent>();

		public void RegisterCollider(UnityEvent uEvent) {
			Debug.Assert(uEvent != null);
			events.Add(uEvent);
		}

		public void DeleteCollider(UnityEvent uEvent) {
			var result = events.Remove(uEvent);
			Debug.Assert(result);   // Deleting event that is not in list => unwanted call
		}

		private void Update() {
			if (PauseMenu.GameIsPaused) { return; }
			if (Input.GetButtonDown("Interact")) { interact(); };
		}

		// Invokes last added event
		private void interact() {
			if (events.Count != 0) {
				events[events.Count - 1].Invoke();
			}
		}
	}
}