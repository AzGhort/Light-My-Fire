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
			SceneManager.LoadScene(newGameScene);
		}

		public void LoadControlsScene() {
			SceneManager.LoadScene(controlsScene);
		}

		public void LoadAboutScene() {
			SceneManager.LoadScene(aboutScene);
		}

		public void QuitGame() {
			Debug.Log("Quitting game!");
			Application.Quit();
		}
	}
}