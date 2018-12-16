using UnityEngine;

namespace LightMyFire
{
	public class PauseMenu : MonoBehaviour {
		public static bool GameIsPaused = false;
		public static bool PauseProhibited = true;

		[SerializeField] private GameObject pauseMenuUi;
		[SerializeField] private SceneField mainMenuScene;

		private void Update() {
			if (!PauseProhibited && Input.GetButtonDown("Pause")) {
				if (GameIsPaused) { ResumeGame(); }
				else { PauseGame(); }
			}
		}

		public void ResumeGame() {
			Time.timeScale = 1f;
			pauseMenuUi.SetActive(false);
			GameIsPaused = false;
		}

		public void PauseGame() {
			Time.timeScale = 0f;
			pauseMenuUi.SetActive(true);
			GameIsPaused = true;
		}

		public void LoadMenu() {
			Time.timeScale = 1f;
			GameIsPaused = false;
			LevelChangerSingleton.LoadScene(mainMenuScene);
		}

		public void QuitGame() {
			Debug.Log("Quitting game!");
			Application.Quit();
		}
	}
}