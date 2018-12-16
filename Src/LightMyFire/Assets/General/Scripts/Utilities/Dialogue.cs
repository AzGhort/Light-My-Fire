using UnityEngine;

namespace LightMyFire
{
	[System.Serializable]
	public class Dialogue : MonoBehaviour
	{
		[SerializeField] private TextAsset dialogueText;
		private DialogueDisplay display;

		private void Awake() { display = FindObjectOfType<DialogueDisplay>(); }

		public void StartDialogue() { display.StartDialogue(dialogueText); }
	}
}
