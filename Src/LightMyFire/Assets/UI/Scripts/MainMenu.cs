using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

        private void Update() {
            if (LevelChangerSingleton.Loading) { return; }
            if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Interact")) {
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}