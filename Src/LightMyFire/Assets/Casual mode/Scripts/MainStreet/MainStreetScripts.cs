using UnityEngine;

namespace LightMyFire
{
	public class MainStreetScripts : MonoBehaviour {

		[SerializeField] private SceneField canalScene;

		public void EnterCanal() {
			LevelChangerSingleton.LoadScene(canalScene);
		}

		public void NarratorVisited() {
			GameState.MainStreetNarratorVisited = true;
		}

	}
}