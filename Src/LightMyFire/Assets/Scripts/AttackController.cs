using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{ 
    enum AttackType
    {
        CLAW_SWIPE, TAIL_SLASH 
    }

    /// <summary>
    /// Class for controlling short ranged attacks, max one attack is in progress at any time.
    /// </summary>
    class AttackController
    {
        bool attacking = false;
        GameObject attack;
        Transform attackPos;
        float openingEnd;
        float attackEnd;

        public bool IsAttacking()
        {
            return attacking;
        }
        public void SetNewAttacker(GameObject attack, Transform attackPos, float attackOpening)
        {
            attack.GetComponent<Collider2D>().transform.position = attackPos.position;
            this.attack = attack;
            this.attackPos = attackPos;
            openingEnd = Time.time + attackOpening;
            attackEnd = Time.time + attack.GetComponent<Attacker>().GetAttackTime();
        }
        public void Attack()
        {
            // we got some attack prepared
            if (!attacking && attack != null)
            {
                attacking = true;
                attack = GameObject.Instantiate(attack, attackPos);
            }
            else if (!attacking) return;
            if (Time.time > attackEnd)
            {
                attack = null;
                attacking = false;
                return;
            }
            if (Time.time > openingEnd)
            {
                attack.GetComponent<Collider2D>().gameObject.SetActive(true);
            }            
        }
    }
}
