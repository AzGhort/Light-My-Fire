using LightMyFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Battle_mode.Scripts.Margot.Shooting
{
    class MargotProjectile : MonoBehaviour
    {
        protected Rigidbody2D rgbd;
        [SerializeField] protected float Damage = 1f;
        [SerializeField] protected float Speed = 20f;
        [SerializeField] Sprite crumb1;
        [SerializeField] Sprite crumb2;
        [SerializeField] Sprite crumb3;

        public virtual void Shoot() { }
        void Start()
        {
            rgbd = GetComponent<Rigidbody2D>();
            var spriteRenderer = GetComponent<SpriteRenderer>();
            float f = UnityEngine.Random.Range(0f, 1f);
            if (f <= 0.33f)
            {
                spriteRenderer.sprite = crumb1;
            }
            else if (f <= 0.66f)
            {
                spriteRenderer.sprite = crumb2;
            }
            else
            {
                spriteRenderer.sprite = crumb3;
            }
            Shoot();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ignore platforms and other projectiles
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
            {
                var player = collision.GetComponent<PlayerHealthManager>();
                if (player) { player.TakeDamage(Damage); }

                Destroy(gameObject);
            }

        }
    }
}
