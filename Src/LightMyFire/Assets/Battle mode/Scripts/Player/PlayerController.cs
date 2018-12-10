using UnityEngine;
using UnityEngine.Events;

namespace LightMyFire
{
	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private float runSpeed = 40f;                              // Maximum speed
		[SerializeField] [Range(0, 1)] private float crouchSpeed = .4f;             // Amount of maxSpeed applied to crouching movement. 1 = 100%
		[SerializeField] private float jumpForce = 700f;                            // Amount of force added when the player jumps.
		[SerializeField] [Range(0, .3f)] private float movementSmoothing = .05f;    // How much to smooth out the movement
		[SerializeField] private bool airControl = false;                           // Whether or not a player can steer while jumping;
		[SerializeField] private LayerMask groundMask;                              // A mask determining what is ground to the character
		[SerializeField] private Transform groundCheck;                             // A position marking where to check if the player is grounded.
		[SerializeField] private float groundedCheckRadius = .2f; // Radius of the overlap circle to determine if grounded

		[SerializeField] private Transform ceilingCheck;                            // A position marking where to check for ceilings
		[SerializeField] private float ceilingCheckRadius = .2f;  // Radius of the overlap circle to determine if the player can stand up

		[SerializeField] private Collider2D crouchDisableCollider;                  // A collider that will be disabled when crouching

		[Header("Events")]
		[Space]

		public UnityEvent OnLandEvent;
		public BoolEvent OnCrouchEvent;
		private bool wasCrouching = false;

		private bool isGrounded;            // Whether or not the player is grounded.
		private bool isFacingRight = true;  // For determining which way the player is currently facing.

		private Rigidbody2D rigidbody2d;
		private Vector3 velocity = Vector3.zero;

		private void Awake() {
			rigidbody2d = GetComponent<Rigidbody2D>();
			Debug.Assert(rigidbody2d);

			if (OnLandEvent == null) { OnLandEvent = new UnityEvent(); }
			if (OnCrouchEvent == null) { OnCrouchEvent = new BoolEvent(); }
		}

		private void FixedUpdate() {
			bool wasGrounded = isGrounded;
			isGrounded = false;

			// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
			// This can be done using layers instead but Sample Assets will not overwrite your project settings.
			Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedCheckRadius, groundMask);
			for (int i = 0; i < colliders.Length; i++) {
				if (colliders[i].gameObject != gameObject) {
					isGrounded = true;
					if (!wasGrounded) { OnLandEvent.Invoke(); }
				}
			}
		}


		public void Move(float move, bool crouch, bool jump) {
			move *= runSpeed;

			// Checks if its possible to stand up (from crouching) by testing ceiling
			if (isGrounded && !crouch) {
				// TODO - Potentially better quality check (explicit rectangle)
				if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, groundMask)) { crouch = true; }
			}

			// Controls ground and air horizontal movement
			if (isGrounded || airControl) {
				if (crouch) {
					if (!wasCrouching) {
						wasCrouching = true;
						OnCrouchEvent.Invoke(true);
					}

					move *= crouchSpeed;    // Slow movement speed for crouching

					if (crouchDisableCollider != null) { crouchDisableCollider.enabled = false; }
				}
				else {
					if (wasCrouching) {
						wasCrouching = false;
						OnCrouchEvent.Invoke(false);
					}

					if (crouchDisableCollider != null) { crouchDisableCollider.enabled = true; }
				}

				// TODO - Port velocity based movement to AddForce with reset variation
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2d.velocity.y);
				// And then smoothing it out and applying it to the character
				rigidbody2d.velocity = Vector3.SmoothDamp(rigidbody2d.velocity, targetVelocity, ref velocity, movementSmoothing);

				// If the input is moving the player right and the player is facing left...
				if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight)) { Flip(); }
			}

			// Add a vertical force to the player => jump
			if (isGrounded && jump) {
				isGrounded = false;
				rigidbody2d.AddForce(new Vector2(0f, jumpForce));
			}
		}

		// Flips player orientation by rotation
		private void Flip() {
			isFacingRight = !isFacingRight;
			transform.Rotate(0f, 180f, 0f);
		}
	}
}