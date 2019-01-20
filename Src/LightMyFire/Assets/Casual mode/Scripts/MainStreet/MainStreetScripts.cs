using System.Collections;
using UnityEngine;

namespace LightMyFire
{
	public class MainStreetScripts : MonoBehaviour {

		[SerializeField] private SceneField canalScene;
        [SerializeField] private SceneField sideStreetScene;
        [SerializeField] private DigitalRuby.RainMaker.RainScript2D rain;

		public void EnterCanal() {
			LevelChangerSingleton.LoadScene(canalScene);
		}

        public void EnterSideStreet() {
            LevelChangerSingleton.LoadScene(sideStreetScene);
        }

		public void NarratorVisited() {
			GameState.MainStreetNarratorVisited = true;
		}

        public void StartRain() {
            GameState.Raining = true;
            StartCoroutine(rainStartUp());
        }

        private IEnumerator rainStartUp() {
            rain.gameObject.SetActive(true);
            rain.RainIntensity = 0;
            while (rain.RainIntensity < 0.5f) {
                rain.RainIntensity += 0.05f;
                yield return new WaitForSeconds(0.5f);
            }
        }

	}
}