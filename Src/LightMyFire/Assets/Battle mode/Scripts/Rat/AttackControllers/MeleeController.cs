using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{ 
    enum MeleeAttackType
    {
        CLAW_SWIPE, TAIL_SLASH 
    }

    /// <summary>
    /// Class for controlling short ranged attacks, max one attack is in progress at any time.
    /// </summary>
    class MeleeController
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
        public void SetNewAttack(GameObject attack, Transform attackPos, float attackOpening)
        {
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
                // TO-DO animation
            }
            // no attack prepared
            else if (!attacking) return;
            // attack is ending
            if (Time.time > attackEnd)
            {
                attack = null;
                attacking = false;
                return;
            }
            // end of attack opening - damage
            if (Time.time > openingEnd)
            {
                attack.GetComponent<Attacker>().DoDamage();
            }            
        }
    }
}
