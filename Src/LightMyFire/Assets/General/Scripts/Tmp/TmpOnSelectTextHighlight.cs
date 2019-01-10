using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace LightMyFire
{
    public class TmpOnSelectTextHighlight : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private TextMeshProUGUI tmproText;
        [SerializeField] private Material highlightMaterial;

        private Material baseMaterial;

        public void OnSelect(BaseEventData eventData) {
            baseMaterial = tmproText.fontSharedMaterial;
            tmproText.fontSharedMaterial = highlightMaterial;
        }

        public void OnDeselect(BaseEventData eventData) {
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
