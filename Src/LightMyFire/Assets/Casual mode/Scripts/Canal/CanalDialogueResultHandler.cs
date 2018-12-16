using System.Collections;
using UnityEngine;

namespace LightMyFire
{
	public class CanalDialogueResultHandler : MonoBehaviour
	{
		[SerializeField] private SceneField ratFightScene;
		[SerializeField] private GameObject ohryzek;

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
				StartCoroutine("waitForRepeatedOhryzekDialog");
			}
		}

		private IEnumerator waitForRepeatedOhryzekDialog() {
			ohryzek.SetActive(false);
			yield return new WaitForSeconds(5f);
			ohryzek.SetActive(true);
		}

	}
}