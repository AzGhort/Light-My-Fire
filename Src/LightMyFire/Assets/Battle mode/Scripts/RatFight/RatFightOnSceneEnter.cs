using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
	public class RatFightOnSceneEnter : MonoBehaviour
	{
		private void Start() {
            MusicPlayerSingleton.Instance.HandleLoadedScene(MusicPlayerSingleton.MusicType.Battle);
			GameState.LastSceneName = SceneManager.GetActiveScene().name;
		}
	}
}