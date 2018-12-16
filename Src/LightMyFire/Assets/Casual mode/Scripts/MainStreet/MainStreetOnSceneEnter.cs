using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
	public class MainStreetOnSceneEnter : MonoBehaviour
	{
		[SerializeField] private SceneField introTextScene;
		[SerializeField] private SceneField canalScene;

		[SerializeField] private Transform introTextEntry;
		[SerializeField] private Transform canalEntry;

		[SerializeField] private GameObject vajgl;
		[SerializeField] private GameObject narrator;

		private void Start() {
			string lastSceneName = GameState.LastSceneName;
			if (introTextScene.SceneName.EndsWith(lastSceneName)) {
				vajgl.transform.position = introTextEntry.position;
			} else {
				vajgl.transform.position = canalEntry.position;
			}

			GameState.LastSceneName = SceneManager.GetActiveScene().name;

			if (GameState.MainStreetNarratorVisited) { narrator.SetActive(false); }
		}
	}
}