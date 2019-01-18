using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
    public class MainStreetOnSceneEnter : MonoBehaviour
    {
        [SerializeField] private SceneField mainStreet;
        [SerializeField] private SceneField introTextScene;
        [SerializeField] private SceneField canalScene;
        [SerializeField] private SceneField sideStreetScene;
        [SerializeField] private SceneField margotFightScene;

        [SerializeField] private Transform margotInteractionEntry;
        [SerializeField] private Transform introTextEntry;
        [SerializeField] private Transform canalEntry;
        [SerializeField] private Transform sideStreetEntry;

        [SerializeField] private GameObject vajgl;
        [SerializeField] private GameObject margotak;
        [SerializeField] private GameObject narrator;
        [SerializeField] private GameObject rain;

        private void Start() {
            string lastSceneName = GameState.LastSceneName;
            if (introTextScene.SceneName.EndsWith(lastSceneName)) {
                vajgl.transform.position = introTextEntry.position;
            }
            else if (canalScene.SceneName.EndsWith(lastSceneName)) {
                vajgl.transform.position = canalEntry.position;
            }
            else if (sideStreetScene.SceneName.EndsWith(lastSceneName)) {
                vajgl.transform.position = sideStreetEntry.position;
            }
            else if (mainStreet.SceneName.EndsWith(lastSceneName) ||
                margotFightScene.SceneName.EndsWith(lastSceneName)) {
                vajgl.transform.position = margotInteractionEntry.position;
            }

            GameState.LastSceneName = SceneManager.GetActiveScene().name;

            if (GameState.MainStreetNarratorVisited) { narrator.SetActive(false); }
            if (GameState.MargotakKilled || !GameState.MargotakMainStreet) { margotak.SetActive(false); }
            if (GameState.Raining) { rain.SetActive(true); }
        }
    }
}