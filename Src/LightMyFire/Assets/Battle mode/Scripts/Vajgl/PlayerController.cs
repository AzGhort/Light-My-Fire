using UnityEngine;

namespace LightMyFire
{
	[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private float runSpeed = 40f;                              // Maximum speed
		[SerializeField] private float jumpForce = 700f;            // Amount of force added when the player jumps.

		[SerializeField] [Range(0, .3f)] private float movementSmoothing = .05f;    // How much to smooth out the movement

		[SerializeField] private LayerMask groundMask;              // A mask determining what is ground to the character
		[SerializeField] private Transform groundCheck;             // A position marking where to check if the player is grounded
		[SerializeField] private float groundedCheckRadius = .2f;   // Radius of the overlap circle to determine if grounded

		// Necessary components of player game object
		private Rigidbody2D rigidbody2d;
		private Animator animator;

		private Collider2D[] groundCheckColliders = new Collider2D[1];
		private Vector2 velocity = Vector2.zero;

		// State variables
		private bool isGrounded = false;
		private bool isFacingRight = true;

		// Input
		private float horizontalMovement = 0f;
		private bool jump = false;

		private void Awake() {
			rigidbody2d = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();

			Debug.Assert(rigidbody2d);
			Debug.Assert(animator);
		}

		private void Update() {
			if (PauseMenu.GameIsPaused) { return; }

			horizontalMovement = Input.GetAxisRaw("Horizontal");
			if (Input.GetButtonDown("Jump")) { jump = true; }
		}

		private void FixedUpdate() {
			checkGround();
			move();
		}

		private void checkGround() {
			bool wasGrounded = isGrounded;
			isGrounded = false;

			// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
			int groundCollisions = Physics2D.OverlapCircleNonAlloc(groundCheck.position, groundedCheckRadius, groundCheckColliders, groundMask);
			if (groundCollisions > 0) {
				isGrounded = true;
				if (!wasGrounded) {
					jump = false;
					animator.SetBool("IsJumping", false);
				}
			}
		}

		private void move() {
			// Movement control

			// Move the character by finding the target velocity
			float xTargetVelocity;
			if (horizontalMovement == 0f && isGrounded) {
				// Stop movement in x axis
				xTargetVelocity = 0f;
			}
			else {
				xTargetVelocity = horizontalMovement * Time.fixedDeltaTime * runSpeed * 10f;
			}
			Vector2 targetVelocity = new Vector2(xTargetVelocity, rigidbody2d.velocity.y);

			// Smoothing out movement and apply it to the character
			rigidbody2d.velocity = Vector2.SmoothDamp(rigidbody2d.velocity, targetVelocity, ref velocity, movementSmoothing);
			animator.SetFloat("Speed", Mathf.Abs(rigidbody2d.velocity.x));

			// Flip player if movement direction is different than current orientation
			if ((xTargetVelocity > 0 && !isFacingRight) || (xTargetVelocity < 0 && isFacingRight)) { Flip(); }

			//Add a vertical force to the player => jump
			if (isGrounded && jump) {
				animator.SetBool("IsJumping", true);
				rigidbody2d.AddForce(new Vector2(0f, jumpForce));
				isGrounded = false;
				jump = false;
			}
		}

		// Flips player orientation by rotation
		private void Flip() {
			transform.Rotate(0f, 180f, 0f);
			isFacingRight = !isFacingRight;
		}
	}
}