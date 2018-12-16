using UnityEngine;
using UnityEngine.Events;

namespace LightMyFire
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class EnemyHealthManager : MonoBehaviour
	{
		[SerializeField] private float maxHealth = 100;
		[SerializeField] private FloatEvent healthBar;
		[SerializeField] private FloatEvent onChangeHealth;
		[SerializeField] private UnityEvent enemyDeath;

		private float currentHealth;

		public void TakeDamage(float damage) {
			if (currentHealth <= 0) { return; }

			currentHealth -= damage;
			healthBar.Invoke(currentHealth / maxHealth);
			if (currentHealth <= 0) { enemyDeath.Invoke(); }
			else if (onChangeHealth != null) { onChangeHealth.Invoke(-damage); }
		}

		private void Awake() {
			currentHealth = maxHealth;
			healthBar.Invoke(currentHealth / maxHealth);
		}
	}
}

