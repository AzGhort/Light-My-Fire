using UnityEngine;

// TODO Cheated by ProjectSettings -> Script order execution (PlayerController before PlayerMovement)

namespace LightMyFire
{
	[RequireComponent(typeof(Animator))]
	public class PlayerMovement : MonoBehaviour
	{

		[SerializeField] private PlayerController controller;

		private Animator animator;

		private float horizontalMove = 0f;
		private bool crouch = false;
		private bool jump = false;

		public void OnLanding() {
			animator.SetBool("IsJumping", false);
		}

		public void OnCrouchingChange(bool isCrouching) {
			animator.SetBool("IsCrouching", isCrouching);
		}


		private void Awake() {
			animator = GetComponent<Animator>();
			Debug.Assert(animator);
		}

		private void Update() {
			if (PauseMenu.GameIsPaused) { return; }

			horizontalMove = Input.GetAxisRaw("Horizontal");
			animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

			if (Input.GetButtonDown("Jump")) {
				jump = true;
				animator.SetBool("IsJumping", true);
			}

			if (Input.GetButtonDown("Crouch")) { crouch = true; }
			else if (Input.GetButtonUp("Crouch")) { crouch = false; }
		}

		private void FixedUpdate() {
			controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
			jump = false;
		}

	}
}