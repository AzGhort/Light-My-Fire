using UnityEngine;

namespace LightMyFire
{
    public class MainMenuOnSceneEnter : MonoBehaviour
    {
        void Start() {
            MusicPlayerSingleton.Instance.HandleLoadedScene(MusicPlayerSingleton.MusicType.Menu);
        }
    }
}