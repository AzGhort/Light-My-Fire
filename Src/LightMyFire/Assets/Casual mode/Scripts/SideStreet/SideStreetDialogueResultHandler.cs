using UnityEngine;

namespace LightMyFire
{
    public class SideStreetDialogueResultHandler : MonoBehaviour
    {
        [SerializeField] private SceneField margotFightScene;

        private void Start() {
            FindObjectOfType<DialogueDisplay>().AddListener(handleDialogueResult);
        }

        private void handleDialogueResult(string result) {
            if (result == "MargotFight") {
                Debug.Log("Dialog result handled - MargotFight");
            } else if (result == "GameFinished") {
                // TODO GameFinished
                Debug.Log("GameFinished");
            }
        }
    }
}