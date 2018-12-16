using UnityEngine;

namespace LightMyFire
{
	[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
	public class PlayerCasualController : MonoBehaviour
	{
		[SerializeField] private float speed;

		// Necessary components of player game object
		private Rigidbody2D rigidbody2d;
		private Animator animator;

		private float horizontalMovement = 0f;
		private float verticalMovement = 0f;
		private bool isFacingRight = true;

		private void Awake() {
			rigidbody2d = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();

			Debug.Assert(rigidbody2d);
			Debug.Assert(animator);
		}

		void Update() {
			if (PauseMenu.GameIsPaused) { return; }
			if (GameState.PlayerFrozen) {
				horizontalMovement = 0;
				verticalMovement = 0;
			}
			else {
				horizontalMovement = Input.GetAxisRaw("Horizontal");
				verticalMovement = Input.GetAxisRaw("Vertical");
			}

			Vector2 velocity = rigidbody2d.velocity;
			bool moreSpeedX = Mathf.Abs(velocity.x) >= Mathf.Abs(velocity.y);
			animator.SetFloat("SpeedHorizontal", Mathf.Abs(velocity.x));
			animator.SetFloat("SpeedY", velocity.y);
			animator.SetBool("SpeedXMoreEqY", moreSpeedX);

			// Flip player if movement direction is different than current orientation
			if (moreSpeedX && ((velocity.x > 0 && !isFacingRight) || (velocity.x < 0 && isFacingRight))) { Flip(); }
		}

		private void FixedUpdate() {
			rigidbody2d.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);
		}

		// Flips player orientation by rotation
		private void Flip() {
			transform.Rotate(0f, 180f, 0f);
			isFacingRight = !isFacingRight;
		}
	}
}