using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
	public class SceneLoader : MonoBehaviour
	{
		[SerializeField] private SceneField scene;

		public void LoadScene() {
			SceneManager.LoadScene(scene);
		}
	}
}