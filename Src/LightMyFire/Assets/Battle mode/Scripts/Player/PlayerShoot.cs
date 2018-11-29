using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace TentativeMaster
{
	public class PlayerShoot : MonoBehaviour
	{

		[SerializeField] private GameObject projectilePrefab;
		[SerializeField] private Transform firePointStanding;
		[SerializeField] private Transform firePointCrouching;

		private bool isCrouching = false;

		public void OnCrouchingChange(bool isCrouching) {
			this.isCrouching = isCrouching;
		}

		private void Update() {
			if (PauseMenu.GameIsPaused) { return; }
			if (Input.GetButtonDown("Fire1")) { fireProjectile(); }
		}

		private void fireProjectile() {
			if (isCrouching) {
				Instantiate(projectilePrefab, firePointCrouching.position, firePointCrouching.rotation);
			}
			else {
				Instantiate(projectilePrefab, firePointStanding.position, firePointStanding.rotation);
			}
		}

	}
}