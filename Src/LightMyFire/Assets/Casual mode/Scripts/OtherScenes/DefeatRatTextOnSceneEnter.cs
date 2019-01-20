using UnityEngine;

namespace LightMyFire
{
    public class DefeatRatTextOnSceneEnter : MonoBehaviour
    {
        void Start() {
            MusicPlayerSingleton.Instance.HandleLoadedScene(MusicPlayerSingleton.MusicType.Menu);
        }
    }
}