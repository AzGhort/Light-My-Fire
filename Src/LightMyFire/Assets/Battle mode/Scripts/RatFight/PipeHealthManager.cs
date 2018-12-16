using UnityEngine;

namespace LightMyFire
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PipeHealthManager : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private FloatEvent onChangeHealth;
        [SerializeField] private Color deadPipe;
        [SerializeField] private static int pipesAlive = 2;

        // damaged pipes sprites
        [SerializeField] private Sprite damaged1;
        [SerializeField] private Sprite damaged2;
        [SerializeField] private Sprite damaged3;
        SpriteRenderer spriteRenderer;

        private float currentHealth;

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (onChangeHealth != null)
            {
                CheckDamage();
                if (currentHealth > 0) { onChangeHealth.Invoke(-damage); }
            }
        }

        private void CheckDamage()
        {
            double percent = currentHealth * 1.0 / maxHealth;            
            if (percent <= 0.0)
            {
                if (spriteRenderer.color != deadPipe)
                {
                    spriteRenderer.sprite = damaged3;
                    spriteRenderer.color = deadPipe;
                    pipesAlive--;
                    PipeDestroyed();
                }
            }
            else if (percent < 0.33)
            {
                spriteRenderer.sprite = damaged2;
            }
            else if (percent < 0.66)
            {
                spriteRenderer.sprite = damaged1;
            }

        }

        private void PipeDestroyed()
        {
            if (pipesAlive == 0)
            {
                var roots = gameObject.scene.GetRootGameObjects();
                foreach (GameObject go in roots)
                {
                    if (go.name == "DropPlatformsButton")
                    {
                        go.SetActive(true);
                    }
                }
            }
        }

        private void Awake()
        {
            currentHealth = maxHealth;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
