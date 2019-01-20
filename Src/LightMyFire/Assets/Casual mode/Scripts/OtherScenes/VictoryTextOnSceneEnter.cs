using UnityEngine;

namespace LightMyFire
{
    public class VictoryTextOnSceneEnter : MonoBehaviour
    {
        void Start() {
            MusicPlayerSingleton.Instance.HandleLoadedScene(MusicPlayerSingleton.MusicType.Menu);
        }
    }
}