using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Shooter : MonoBehaviour
    {
        protected Rigidbody2D rgbd;
        public bool Target;

        public virtual void ShootTargeted()
        {

        }
        public virtual void ShootUntargeted()
        {


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
            if (collision.gameObject.CompareTag("Player"))
            {
                // do damage
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("Boundary"))
            {
                Destroy(gameObject);
            }
        }
    }
}
