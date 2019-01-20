using UnityEngine;

namespace LightMyFire
{
	public class RatFightResultHandler : MonoBehaviour
	{
		[SerializeField] private SceneField onVictoryScene;
		[SerializeField] private SceneField onDeathScene;

		public void OnVictory() {
            MusicPlayerSingleton.Instance.FadeOutOfSong();
			GameState.RatKilled = true;
			LevelChangerSingleton.LoadScene(onVictoryScene);
		}

		public void OnDeath() {
            MusicPlayerSingleton.Instance.FadeOutOfSong();
			LevelChangerSingleton.LoadScene(onDeathScene);
		}
	}
}