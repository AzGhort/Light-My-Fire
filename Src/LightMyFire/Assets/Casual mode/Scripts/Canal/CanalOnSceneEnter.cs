using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
    public class CanalOnSceneEnter : MonoBehaviour
    {
        [SerializeField] private SceneField mainStreetScene;
        [SerializeField] private SceneField ratFightScene;

        [SerializeField] private Transform mainStreetEntry;
        [SerializeField] private Transform ratFightEntry;

        [SerializeField] private GameObject vajgl;
        [SerializeField] private GameObject rat;
        [SerializeField] private GameObject ohryzek;
        [SerializeField] private GameObject bubbleGum;

        private void Start() {
            string lastSceneName = GameState.LastSceneName;
            if (mainStreetScene.SceneName.EndsWith(lastSceneName)) {
                vajgl.transform.position = mainStreetEntry.position;
            }
            else {
                vajgl.transform.position = ratFightEntry.position;
            }

            GameState.LastSceneName = SceneManager.GetActiveScene().name;

            if (GameState.DeadOhryzek || GameState.RatKilled) {
                ohryzek.SetActive(false);
                bubbleGum.SetActive(false);

                if (!GameState.RatKilled) { rat.SetActive(true); }
            }
        }
    }
}