using UnityEngine;
using UnityEngine.Events;

namespace LightMyFire
{
    public class Interactible : MonoBehaviour
    {
        [SerializeField] private UnityEvent uEvent;
        [SerializeField] bool highlightOutOfRange = false;
        [SerializeField] private Animator interactionHighlight = null;

        private void Start() {
            if (interactionHighlight && highlightOutOfRange) {
                interactionHighlight.SetBool("On", true);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                if (interactionHighlight) { interactionHighlight.SetBool("On", true); }
                collision.gameObject.GetComponent<PlayerInteractor>().RegisterCollider(uEvent);
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                if (interactionHighlight && !highlightOutOfRange) { interactionHighlight.SetBool("On", false); }
                collision.gameObject.GetComponent<PlayerInteractor>().DeleteCollider(uEvent);
            }
        }
    }
}