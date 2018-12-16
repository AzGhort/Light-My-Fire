using LightMyFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class RatAttacker : MonoBehaviour
    {
        protected float attackTime;
        protected Transform startPoint;
        [SerializeField] protected int Damage;
        protected Collider2D coll;
        protected SpriteRenderer sprnd;
        public float GetAttackTime()
        {
            return attackTime;
        }             
        public virtual void DoDamage()
        {
        }
        public virtual void Setup() { }
        public void EndAttack()
        {
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.GetComponent<PlayerHealthManager>();
                if (player) { player.TakeDamage(Damage); }
            }
        }
    }
}
