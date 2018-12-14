using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private SceneField newGameScene;
		[SerializeField] private SceneField controlsScene;
		[SerializeField] private SceneField aboutScene;

		public void LoadNewGameScene() {
			LevelChangerSingleton.LoadScene(newGameScene);
		}

		public void LoadControlsScene() {
			LevelChangerSingleton.LoadScene(controlsScene);
		}

		public void LoadAboutScene() {
			LevelChangerSingleton.LoadScene(aboutScene);
		}

		public void QuitGame() {
			Debug.Log("Quitting game!");
			Application.Quit();
		}
	}
}