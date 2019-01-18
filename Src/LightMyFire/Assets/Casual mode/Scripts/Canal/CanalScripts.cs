using UnityEngine;

namespace LightMyFire
{
    public class CanalScripts : MonoBehaviour
    {
        [SerializeField] private SceneField mainStreetScene;
        [SerializeField] private SceneField ratFightScene;

        public void EnterMainStreet() {
            LevelChangerSingleton.LoadScene(mainStreetScene);
        }

        public void EnterRatFightScene() {
            LevelChangerSingleton.LoadScene(ratFightScene);
        }
    }
}