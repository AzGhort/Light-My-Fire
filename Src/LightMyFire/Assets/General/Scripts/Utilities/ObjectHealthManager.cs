using UnityEngine;

namespace LightMyFire
{
	public class ObjectHealthManager : MonoBehaviour
	{
		[SerializeField] private float maxHealth = 100;
		[SerializeField] private FloatEvent onChangeHealth;

		private float currentHealth;

		public void TakeDamage(float damage) {
			currentHealth -= damage;
			if (currentHealth <= 0) { Destroy(gameObject); }
			else if (onChangeHealth != null) { onChangeHealth.Invoke(-damage); }
		}

		private void Awake() {
			currentHealth = maxHealth;
		}
	}
}