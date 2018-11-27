using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace TentativeMaster
{
	public class PlayerProjectile : MonoBehaviour
	{
		[SerializeField] private GameObject impactEffect;
		[SerializeField] private float speed = 20f;
		[SerializeField] private int damage = 40;

		private Rigidbody2D rigidbody2d;

		private void Start() {
			rigidbody2d = GetComponent<Rigidbody2D>();
			rigidbody2d.velocity = transform.right * speed;
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			// Do not destroy on potential hit of player (because of lag/etc)
			if (collision.CompareTag("Player")) { return; }	
			
			Enemy enemy = collision.GetComponent<Enemy>();
			if (enemy) { enemy.TakeDamage(damage); }

			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}