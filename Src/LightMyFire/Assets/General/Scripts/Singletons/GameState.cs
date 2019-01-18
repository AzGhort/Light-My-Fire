using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
	public static class GameState
	{
		public static string LastSceneName = "";	// Empty default

		public static bool PlayerFrozen = false;

		public static bool MainStreetNarratorVisited = false;
        public static bool Raining = false;

        public static bool MargotakKilled = false;
        public static bool MargotakMainStreet = true;
        public static bool MargotakSideStreet = false;

		public static bool RatKilled = false;
        public static bool DeadOhryzek = false;

		public static void ResetGameProgress() {
			LastSceneName = SceneManager.GetActiveScene().name;

            PlayerFrozen = false;

			MainStreetNarratorVisited = false;
            Raining = false;

            MargotakKilled = false;
            MargotakMainStreet = true;
            MargotakSideStreet = false;

			RatKilled = false;
            DeadOhryzek = false;
		}
	}
}