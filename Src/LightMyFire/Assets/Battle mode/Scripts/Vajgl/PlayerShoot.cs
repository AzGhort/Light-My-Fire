using UnityEngine;

namespace LightMyFire
{
	[RequireComponent(typeof(Animator))]
	public class PlayerShoot : MonoBehaviour
	{
		[SerializeField] private GameObject projectilePrefab;
		[SerializeField] private Transform firePoint;

		private Animator animator;

		public void FireProjectile() {
			Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
		}

		public void ReloadProjectile() {
			animator.SetBool("Fire", false);
		}

		private void Awake() {
			animator = GetComponent<Animator>();
			Debug.Assert(animator);
		}

		private void Update() {
			if (PauseMenu.GameIsPaused) { return; }
			if (Input.GetButtonDown("Fire1")) {
				animator.SetBool("Fire", true);
			}
		}
	}
}