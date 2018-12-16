using UnityEngine;

namespace LightMyFire
{
	public class MainStreetDialogueResultHandler : MonoBehaviour
	{
		[SerializeField] private SceneField margotFightScene;

		private void Start() {
			FindObjectOfType<DialogueDisplay>().AddListener(handleDialogueResult);
		}

		private void handleDialogueResult(string result) {
			if (result == "MargotFight") {
				Debug.Log("Dialog result handled - MargotFight");
			}
			else if (result == "MargotHidden") {
				Debug.Log("Dialog result handled - MargotHidden");
			}
		}
	}
}