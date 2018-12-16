using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
	public class RatFightOnSceneEnter : MonoBehaviour
	{
		private void Start() {
			GameState.LastSceneName = SceneManager.GetActiveScene().name;
		}
	}
}