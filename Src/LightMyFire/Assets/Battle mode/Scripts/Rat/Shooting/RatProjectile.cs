using LightMyFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class RatProjectile : MonoBehaviour
    {
        [SerializeField] protected GameObject ImpactEffect;
        protected Rigidbody2D rgbd;
        [SerializeField] public bool Target = true;
        [SerializeField] protected int Damage = 1;
        [SerializeField] protected float Speed = 5f;

        public virtual void ShootTargeted() {     }
        public virtual void ShootUntargeted() {     }
        void Start()
        {
            rgbd = GetComponent<Rigidbody2D>();
            if (Target)
            {
                ShootTargeted();
            }
            else ShootUntargeted();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ignore platforms and other projectiles
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
            {
                var player = collision.GetComponent<PlayerHealthManager>();
                if (player) { player.TakeDamage(Damage); }

                if (ImpactEffect) { Instantiate(ImpactEffect, transform.position, transform.rotation); }
                Destroy(gameObject);
            }

        }
    }
}
