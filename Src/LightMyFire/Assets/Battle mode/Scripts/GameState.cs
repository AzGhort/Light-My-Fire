using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace TentativeMaster
{
	public class GameState
	{
		public GameState(int stateCode, int location) {
			_stateCode = stateCode;
			_location = location;
			_events = new List<IEvent>();
		}

		public static int StateCode {
			get { return _stateCode; }
			set {
				if (value == _stateCode) return;
				_stateCode = value;
				OnStateChanged();
			}
		}

		private static void OnStateChanged() {
			foreach (var e in _events) {
				if (e.ShouldHappen(_location))
					e.Invoke();
			}
		}

		public static void ChangeLocation(int location) {
			_location = location;
		}

		public static void RegisterEvents(params IEvent[] e) {
			_events.AddRange(e);
		}

		private static int _stateCode;
		private static List<IEvent> _events;
		private static int _location;
	}
}