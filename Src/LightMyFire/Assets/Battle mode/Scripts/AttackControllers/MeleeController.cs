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
        float openingEnd;
        float attackEnd;

        public bool IsAttacking()
        {
            return attacking;
        }
        public void SetNewAttack(GameObject attack, Vector3 attackPos, float attackOpening)
        {
            this.attack = GameObject.Instantiate(attack, attackPos, Quaternion.identity);
            this.attack.GetComponent<RatAttacker>().Setup();
            openingEnd = Time.time + attackOpening;
            attackEnd = Time.time + attack.GetComponent<RatAttacker>().GetAttackTime();
            attacking = true;
        }
        public void Attack()
        {
            if (!attacking) return;

            // end of attack opening - damage
            if (Time.time > openingEnd && openingEnd != 0)
            {
                attack.GetComponent<RatAttacker>().DoDamage();
                openingEnd = 0;
            }
            // attack is ending
            else if (Time.time > attackEnd)
            {
                attacking = false;
                attack.GetComponent<RatAttacker>().EndAttack();
            }
        }
    }
}
