using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LightMyFire
{
    public class IntroTextContinueButton : MonoBehaviour
    {
        private void Update() {
            if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Interact") || Input.GetButtonDown("Cancel")) {
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
