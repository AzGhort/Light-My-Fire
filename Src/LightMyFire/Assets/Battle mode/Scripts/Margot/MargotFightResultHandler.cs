using UnityEngine;

namespace LightMyFire
{
    public class MargotFightResultHandler : MonoBehaviour
    {
        [SerializeField] private SceneField onVictoryMainStreetScene;
        [SerializeField] private SceneField onVictorySideStreetScene;
        [SerializeField] private SceneField onDeathScene;

        public void OnVictory() {
            GameState.MargotakKilled = true;
            if (GameState.MargotakMainStreet) { LevelChangerSingleton.LoadScene(onVictoryMainStreetScene); }
            else { LevelChangerSingleton.LoadScene(onVictorySideStreetScene); }
        }

        public void OnDeath() {
            LevelChangerSingleton.LoadScene(onDeathScene);
        }
    }
}
