﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace TentativeMaster
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private int health = 100;

		public void TakeDamage(int damage) {
			health -= damage;
			if (health <= 0) { Destroy(gameObject); }
		}
	}
}

