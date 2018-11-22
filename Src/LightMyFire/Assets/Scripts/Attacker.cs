using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Attacker : MonoBehaviour
    {
        protected float attackTime;
        protected Transform startPoint;
        public float GetAttackTime()
        {
            return attackTime;
        }
        private void Start()
        {
            GetComponent<Collider2D>().gameObject.SetActive(false);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // do some dmg
            }
        }
    }
}
