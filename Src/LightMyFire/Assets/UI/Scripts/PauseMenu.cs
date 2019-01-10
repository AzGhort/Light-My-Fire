using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LightMyFire
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        public static bool PauseProhibited = true;

        [SerializeField] private GameObject pauseMenuUi;
        [SerializeField] private GameObject onPauseSelectedButton;
        [SerializeField] private SceneField mainMenuScene;

        private void Update() {
            if (GameIsPaused) {
                if (Input.GetButtonDown("Pause")) { ResumeGame(); }
                if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Interact")) {
                    EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
                }
            } else if (!PauseProhibited && Input.GetButtonDown("Pause")) { PauseGame(); }
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

            // Reselect button (so we forcefully trigger event OnSelected on specified button)
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(onPauseSelectedButton);
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