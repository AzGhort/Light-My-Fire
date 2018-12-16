﻿using UnityEngine;

namespace LightMyFire
{
	public class RatFightResultHandler : MonoBehaviour
	{
		[SerializeField] private SceneField onVictoryScene;
		[SerializeField] private SceneField onDeathScene;

		public void OnVictory() {
			GameState.KilledRat = true;
			LevelChangerSingleton.LoadScene(onVictoryScene);
		}

		public void OnDeath() {
			LevelChangerSingleton.LoadScene(onDeathScene);
		}

	}
}