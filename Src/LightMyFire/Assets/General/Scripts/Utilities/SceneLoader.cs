using UnityEngine;

namespace LightMyFire
{
	public class SceneLoader : MonoBehaviour
	{
		[SerializeField] private SceneField scene;

		public void LoadScene() {
			LevelChangerSingleton.LoadScene(scene);
		}
	}
}