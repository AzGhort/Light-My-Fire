using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
    public class MargotFightOnSceneEnter : MonoBehaviour
    {
        private void Start() {
            GameState.LastSceneName = SceneManager.GetActiveScene().name;
        }
    }
}
