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

        public void Destroy(Collider2D collision)
        {
            var diff = (gameObject.transform.position - collision.transform.position).normalized;
            var upperRight = (collision.transform.position + new Vector3(collision.bounds.size.x / 2, collision.bounds.size.y / 2, 0)).normalized;
            var upperLeft = (collision.transform.position + new Vector3(-collision.bounds.size.x / 2, collision.bounds.size.y / 2, 0)).normalized;
            var lowerRight = (collision.transform.position + new Vector3(collision.bounds.size.x / 2, -collision.bounds.size.y / 2, 0)).normalized;
            var lowerLeft = (collision.transform.position + new Vector3(-collision.bounds.size.x / 2, -collision.bounds.size.y / 2, 0)).normalized;

            if (diff.y >= upperRight.y)
            {
                gameObject.transform.Rotate(0, 0, -90);
            }
            else if (diff.x >= upperRight.x)
            {
                gameObject.transform.Rotate(0, 0, -180);
            }

            if (ImpactEffect) { Instantiate(ImpactEffect, transform.position, transform.rotation); }
            Destroy(gameObject);
        }
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
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall") 
                || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Projectile"))
            {
                var player = collision.GetComponent<PlayerHealthManager>();
                if (player) { player.TakeDamage(Damage); }

                Destroy(collision);
            }

        }
    }
}
