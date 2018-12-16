using UnityEngine;

namespace LightMyFire
{
	public class CanalScripts : MonoBehaviour
	{
		[SerializeField] private SceneField mainStreetScene;

		public void EnterMainStreet() {
			LevelChangerSingleton.LoadScene(mainStreetScene);
		}

	}
}