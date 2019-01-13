using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
    public class MainStreetOnSceneEnter : MonoBehaviour
    {
        [SerializeField] private SceneField introTextScene;
        [SerializeField] private SceneField canalScene;
        [SerializeField] private SceneField sideStreetScene;

        [SerializeField] private Transform introTextEntry;
        [SerializeField] private Transform canalEntry;
        [SerializeField] private Transform sideStreetEntry;

        [SerializeField] private GameObject vajgl;
        [SerializeField] private GameObject narrator;

        private void Start() {
            string lastSceneName = GameState.LastSceneName;
            if (introTextScene.SceneName.EndsWith(lastSceneName)) {
                vajgl.transform.position = introTextEntry.position;
            }
            else if (canalScene.SceneName.EndsWith(lastSceneName)) {
                vajgl.transform.position = canalEntry.position;
            }
            else {
                vajgl.transform.position = sideStreetEntry.position;
            }

            GameState.LastSceneName = SceneManager.GetActiveScene().name;

            if (GameState.MainStreetNarratorVisited) { narrator.SetActive(false); }
        }
    }
}