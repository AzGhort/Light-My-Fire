using UnityEngine;

namespace LightMyFire
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerProjectile : MonoBehaviour
	{
		[SerializeField] private GameObject impactEffect;
		[SerializeField] private float speed = 20f;
		[SerializeField] private int damage = 40;

		private Rigidbody2D rigidbody2d;

		private void Awake() {
			rigidbody2d = GetComponent<Rigidbody2D>();
			Debug.Assert(rigidbody2d);

			rigidbody2d.velocity = transform.right * speed;
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			// Do not destroy on potential hit of player (because of lag/etc)
			if (collision.CompareTag("Player") || collision.CompareTag("MeleeAttack")) { return; }

			var enemy = collision.GetComponent<EnemyHealthManager>();
			if (enemy) { enemy.TakeDamage(damage); }

			var pipe = collision.GetComponent<PipeHealthManager>();
			if (pipe) { pipe.TakeDamage(damage); }

			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}