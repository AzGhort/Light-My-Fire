using System.Collections;
using UnityEngine;

namespace LightMyFire
{
    public class CanalDialogueResultHandler : MonoBehaviour
    {
        [SerializeField] private SceneField mainStreetScene;
        [SerializeField] private SceneField ratFightScene;

        [SerializeField] private GameObject ohryzekFirstDialogue;
        [SerializeField] private GameObject ohryzekLaterDialogue;

        [SerializeField] private GameObject exitDefault;
        [SerializeField] private GameObject exitDecision;

        private void Start() {
            FindObjectOfType<DialogueDisplay>().AddListener(handleDialogueResult);
        }

        private void handleDialogueResult(string result) {
            if (result == "RatFight") {
                Debug.Log("Dialog result handled - RatFight");
                MusicPlayerSingleton.Instance.FadeOutOfSong();
                LevelChangerSingleton.LoadScene(ratFightScene);
            }
            else if (result == "LookAround") {
                Debug.Log("Dialog result handled - LookAround");
                StartCoroutine(lookAroundCanal());
            }
            else if (result == "LookAgain") {
                Debug.Log("Dialog result handled - LookAgain");
                StartCoroutine(lookAroundCanalAgain());
            }
            else if (result == "LetDieAndEscape") {
                Debug.Log("Dialog result handled - LetDieAndEscape");
                GameState.MargotakMainStreet = false;
                GameState.MargotakSideStreet = true;
                GameState.DeadOhryzek = true;
                LevelChangerSingleton.LoadScene(mainStreetScene);
            }
        }

        private IEnumerator lookAroundCanal() {
            exitDefault.SetActive(false);
            exitDecision.SetActive(true);
            ohryzekFirstDialogue.SetActive(false);
            yield return new WaitForSeconds(5f);
            ohryzekLaterDialogue.SetActive(true);
        }

        private IEnumerator lookAroundCanalAgain() {
            ohryzekLaterDialogue.SetActive(false);
            yield return new WaitForSeconds(5f);
            ohryzekLaterDialogue.SetActive(true);
        }
    }
}