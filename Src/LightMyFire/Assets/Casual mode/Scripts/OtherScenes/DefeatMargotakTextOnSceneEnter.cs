using UnityEngine;

namespace LightMyFire
{
    public class DefeatMargotakTextOnSceneEnter : MonoBehaviour
    {
        void Start() {
            MusicPlayerSingleton.Instance.HandleLoadedScene(MusicPlayerSingleton.MusicType.Menu);
        }
    }
}