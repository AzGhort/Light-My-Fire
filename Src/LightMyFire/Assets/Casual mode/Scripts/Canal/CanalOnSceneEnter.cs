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

		private void Start() {
			string lastSceneName = GameState.LastSceneName;
			if (mainStreetScene.SceneName.EndsWith(lastSceneName)) {
				vajgl.transform.position = mainStreetEntry.position;
			}
			else {
				vajgl.transform.position = ratFightEntry.position;
			}

			GameState.LastSceneName = SceneManager.GetActiveScene().name;
		}
	}
}