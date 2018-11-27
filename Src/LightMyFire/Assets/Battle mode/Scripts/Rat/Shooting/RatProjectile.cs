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
        [SerializeField] public int Damage = 40;
        [SerializeField] public float Speed = 3f;

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
            if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Projectile"))
            {
                return;
            }
            
            Player player = collision.GetComponent<Player>();
            if (player) { /*player.TakeDamage(damage);*/ }

            // TODO - remove check for optimisation
            if (ImpactEffect) { Instantiate(ImpactEffect, transform.position, transform.rotation); }
            Destroy(gameObject);
        }
    }
}
