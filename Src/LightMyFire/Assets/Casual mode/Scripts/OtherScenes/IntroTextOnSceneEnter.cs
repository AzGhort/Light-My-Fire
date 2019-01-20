using UnityEngine;

namespace LightMyFire
{
    public class IntroTextOnSceneEnter : MonoBehaviour
    {
        void Start() {
            MusicPlayerSingleton.Instance.HandleLoadedScene(MusicPlayerSingleton.MusicType.Casual);
        }
    }
}