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
			events.Remove(uEvent);
		}

		private void Update() {
			if (PauseMenu.GameIsPaused) { return; }
			if (!GameState.PlayerFrozen && Input.GetButtonDown("Interact")) { interact(); };
		}

		// Invokes last added event
		private void interact() {
			if (events.Count != 0) {
				var lastEvent = events[events.Count - 1];
				events.RemoveAt(events.Count - 1);
				lastEvent.Invoke();
			}
		}
	}
}