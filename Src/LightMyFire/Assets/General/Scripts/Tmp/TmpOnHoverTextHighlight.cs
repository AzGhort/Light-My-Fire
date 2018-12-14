using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace LightMyFire
{
	public class TmpOnHoverTextHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private TextMeshProUGUI tmproText;
		[SerializeField] private Material highlightMaterial;

		private Material baseMaterial;

		public void OnPointerEnter(PointerEventData eventData) {
			baseMaterial = tmproText.fontSharedMaterial;
			tmproText.fontSharedMaterial = highlightMaterial;
		}

		public void OnPointerExit(PointerEventData eventData) {
			tmproText.fontSharedMaterial = baseMaterial;
		}

		private void Awake() {
			baseMaterial = tmproText.fontSharedMaterial;
		}

		private void OnEnable() {
			tmproText.fontSharedMaterial = baseMaterial;
		}
	}
}