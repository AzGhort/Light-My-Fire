using UnityEngine;
using UnityEngine.Events;

namespace LightMyFire
{
	public class PlayerHealthManager : MonoBehaviour
	{
		[SerializeField] private float maxHealth = 100;
		[SerializeField] private FloatEvent healthBar;
		[SerializeField] private FloatEvent onChangeHealth;
		[SerializeField] private UnityEvent playerDeath;

		private float currentHealth;

		public void TakeDamage(float damage) {
			if (currentHealth <= 0) { return; }

			currentHealth -= damage;
			healthBar.Invoke(currentHealth / maxHealth);
			if (currentHealth <= 0) { playerDeath.Invoke(); }
			else if (onChangeHealth != null) { onChangeHealth.Invoke(-damage); }
		}

		private void Awake() {
			currentHealth = maxHealth;
			healthBar.Invoke(currentHealth / maxHealth);
		}
	}
}