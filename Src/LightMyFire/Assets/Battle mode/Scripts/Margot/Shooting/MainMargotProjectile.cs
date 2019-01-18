using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Battle_mode.Scripts.Margot.Shooting
{
    class MainMargotProjectile : MargotProjectile
    {
        public override void Shoot()
        {
            GameObject pl = GameObject.Find("Vajgl");
            // better safe than sorry
            if (pl == null) return;
            Transform player = pl.transform;

            Vector2 toPlayer = (new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y))).normalized;
            rgbd.velocity = Speed * toPlayer;
        }
    }
}
