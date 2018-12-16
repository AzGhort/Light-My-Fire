﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
	public static class GameState
	{
		public static string LastSceneName = "";	// Empty default

		public static bool PlayerFrozen = false;

		public static bool MainStreetNarratorVisited = false;
		public static bool KilledRat = false;

		public static void ResetGameProgress() {
			LastSceneName = SceneManager.GetActiveScene().name;
			MainStreetNarratorVisited = false;
			KilledRat = false;
		}
	}
}