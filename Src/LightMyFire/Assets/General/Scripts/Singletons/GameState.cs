using UnityEngine;

namespace LightMyFire
{
	public static class GameState
	{
		public static bool PlayerFrozen = false;

		public static void FreezePlayer(bool freeze) {
			PlayerFrozen = freeze;
			if (freeze) { Input.ResetInputAxes(); }
		}

	}
}