using UnityEngine;

namespace LightMyFire
{
	public class PopUpText : MonoBehaviour
	{
		[TextArea(3, 10)] public string ActorName;
		[TextArea(3, 10)] public string Text;

		private DialogueDisplay display;
		private void Awake() { display = FindObjectOfType<DialogueDisplay>(); }

		public void StartPopUp() {
			display.StartPopUp(ActorName, Text);
		}

		public void EndPopUp() {
			display.EndPopUp();
		}
	}
}