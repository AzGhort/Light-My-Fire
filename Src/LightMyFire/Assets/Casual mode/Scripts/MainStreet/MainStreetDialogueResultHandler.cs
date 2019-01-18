using UnityEngine;

namespace LightMyFire
{
	public class MainStreetDialogueResultHandler : MonoBehaviour
	{
		[SerializeField] private SceneField margotFightScene;
        [SerializeField] private SceneField mainStreet;

		private void Start() {
			FindObjectOfType<DialogueDisplay>().AddListener(handleDialogueResult);
		}

		private void handleDialogueResult(string result) {
			if (result == "MargotFight") {
				Debug.Log("Dialog result handled - MargotFight");
                LevelChangerSingleton.LoadScene(margotFightScene);
			}
			else if (result == "MargotHidden") {
				Debug.Log("Dialog result handled - MargotHidden");
                GameState.Raining = false;
                GameState.MargotakMainStreet = false;
                LevelChangerSingleton.LoadScene(mainStreet);
			}
		}
	}
}