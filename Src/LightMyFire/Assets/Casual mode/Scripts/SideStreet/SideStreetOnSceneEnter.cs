using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
    public class SideStreetOnSceneEnter : MonoBehaviour
    {
        [SerializeField] private SceneField mainStreetScene;
		[SerializeField] private SceneField margotakFightScene;

		[SerializeField] private Transform mainStreetEntry;
		[SerializeField] private Transform margotakFightEntry;

		[SerializeField] private GameObject vajgl;

		private void Start() {
			string lastSceneName = GameState.LastSceneName;
			if (mainStreetScene.SceneName.EndsWith(lastSceneName)) {
				vajgl.transform.position = mainStreetEntry.position;
			}
			else {
				vajgl.transform.position = margotakFightEntry.position;
			}

			GameState.LastSceneName = SceneManager.GetActiveScene().name;
		}
    }
}