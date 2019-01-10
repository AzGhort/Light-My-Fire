using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LightMyFire
{
    public class AboutMenu : MonoBehaviour
    {
        private void Update() {
            if (LevelChangerSingleton.Loading) { return; }
            if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Interact") || Input.GetButtonDown("Cancel")) {
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
