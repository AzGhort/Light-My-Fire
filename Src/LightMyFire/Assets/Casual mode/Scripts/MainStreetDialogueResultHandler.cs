using UnityEngine;

namespace LightMyFire
{
	public class MainStreetDialogueResultHandler : MonoBehaviour
	{
		[SerializeField] private SceneField krysaFightScene;

		private void Start() {
			FindObjectOfType<DialogueDisplay>().AddListener(HandleDialogueResult);
		}

		private void HandleDialogueResult(string result) {
			if (result == "KrysaFight") {
				Debug.Log("Dialog result handled - KrysaFight");
				LevelChangerSingleton.LoadScene(krysaFightScene);
			}
			else if (result == "MargotakHidden") {
				Debug.Log("Dialog result handled - MargotakHidden");
			}
		}
	}
}