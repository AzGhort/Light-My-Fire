using UnityEngine;

namespace LightMyFire
{
    public class SideStreetDialogueResultHandler : MonoBehaviour
    {
        [SerializeField] private SceneField margotFightScene;
        [SerializeField] private SceneField victoryScreenScene;

        private void Start() {
            FindObjectOfType<DialogueDisplay>().AddListener(handleDialogueResult);
        }

        private void handleDialogueResult(string result) {
            if (result == "MargotFight") {
                Debug.Log("Dialog result handled - MargotFight");
                LevelChangerSingleton.LoadScene(margotFightScene);
            } else if (result == "GameFinished") {
                Debug.Log("Dialog result handled - GameFinished");
                LevelChangerSingleton.LoadScene(victoryScreenScene);
            }
        }
    }
}