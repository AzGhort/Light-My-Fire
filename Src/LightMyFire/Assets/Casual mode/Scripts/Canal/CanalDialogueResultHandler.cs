using UnityEngine;

namespace LightMyFire
{
	public class CanalDialogueResultHandler : MonoBehaviour
	{
		[SerializeField] private SceneField ratFightScene;

		[SerializeField] private GameObject ohryzek;
		[SerializeField] private PopUpText mainStreetEntrancePopUp;

		private void Start() {
			FindObjectOfType<DialogueDisplay>().AddListener(handleDialogueResult);
		}

		private void handleDialogueResult(string result) {
			if (result == "RatFight") {
				Debug.Log("Dialog result handled - RatFight");
				LevelChangerSingleton.LoadScene(ratFightScene);
			}
			else if (result == "OhryzekDead") {
				Debug.Log("Dialog result handled - OhryzekDead");
				Destroy(ohryzek);
				mainStreetEntrancePopUp.Text = "Je na čase vypadnout - začíná to tu páchnout po jabkách...\n\n(vstoupit = e)";
			}
		}

	}
}