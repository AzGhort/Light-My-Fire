using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

	[SerializeField] private GameObject impactEffect;
	[SerializeField] private float speed = 20f;
	[SerializeField] private int damage = 40;

	private Rigidbody2D rigidbody2d;

	private void Start () {
		rigidbody2d = GetComponent<Rigidbody2D>();
		rigidbody2d.velocity = transform.right * speed;
	}

	private void OnTriggerEnter2D(Collider2D collision) {

		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy) { enemy.TakeDamage(damage); }

		// TODO - remove check for optimisation
		if (impactEffect) { Instantiate(impactEffect, transform.position, transform.rotation); }
		Destroy(gameObject);
	}
}
