using UnityEngine;
using UnityEngine.EventSystems;

namespace LightMyFire
{
    [RequireComponent(typeof(EventSystem))]
    public class DisableCursor : MonoBehaviour
    {
        private GameObject lastSelected;

        void Awake() {
            lastSelected = new GameObject();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update() {
            if (EventSystem.current.currentSelectedGameObject == null) {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            } else {
                lastSelected = EventSystem.current.currentSelectedGameObject;
            }
        }
    }
}
