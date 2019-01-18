using UnityEngine;

namespace LightMyFire
{
    public class MargotFightResultHandler : MonoBehaviour
    {
        [SerializeField] private SceneField onVictoryScene;
        [SerializeField] private SceneField onDeathScene;

        public void OnVictory() {
            GameState.MargotakKilled = true;
            LevelChangerSingleton.LoadScene(onVictoryScene);
        }

        public void OnDeath() {
            LevelChangerSingleton.LoadScene(onDeathScene);
        }
    }
}
