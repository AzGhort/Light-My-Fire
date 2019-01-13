using UnityEngine;

namespace LightMyFire
{
	public class MainStreetScripts : MonoBehaviour {

		[SerializeField] private SceneField canalScene;
        [SerializeField] private SceneField sideStreetScene;

		public void EnterCanal() {
			LevelChangerSingleton.LoadScene(canalScene);
		}

        public void EnterSideStreet() {
            LevelChangerSingleton.LoadScene(sideStreetScene);
        }

		public void NarratorVisited() {
			GameState.MainStreetNarratorVisited = true;
		}

	}
}