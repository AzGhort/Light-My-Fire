using UnityEngine;

namespace LightMyFire
{
	public class PlayerHealthManager : MonoBehaviour
	{
		[SerializeField] private float maxHealth = 100;
		[SerializeField] private FloatEvent healthBar;
		[SerializeField] private FloatEvent onChangeHealth;

		private float currentHealth;

		public void TakeDamage(float damage) {
			currentHealth -= damage;
			healthBar.Invoke(currentHealth / maxHealth);
			if (currentHealth <= 0) { Destroy(gameObject); }
			else if (onChangeHealth != null) { onChangeHealth.Invoke(-damage); }
		}

		private void Awake() {
			currentHealth = maxHealth;
			healthBar.Invoke(currentHealth / maxHealth);
		}
	}
}