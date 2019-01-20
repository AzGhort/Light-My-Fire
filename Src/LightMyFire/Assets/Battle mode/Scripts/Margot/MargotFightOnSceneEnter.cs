using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
    public class MargotFightOnSceneEnter : MonoBehaviour
    {
        private void Start() {
            MusicPlayerSingleton.Instance.HandleLoadedScene(MusicPlayerSingleton.MusicType.Battle);
            GameState.LastSceneName = SceneManager.GetActiveScene().name;
        }
    }
}
