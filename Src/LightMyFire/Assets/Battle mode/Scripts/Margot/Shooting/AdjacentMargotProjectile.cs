using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Battle_mode.Scripts.Margot.Shooting
{
    class AdjacentMargotProjectile : MargotProjectile
    {
        public override void Shoot()
        {
            float shift = GetRandomShift();

            GameObject pl = GameObject.Find("Vajgl");
            // better safe than sorry
            if (pl == null) return;
            Transform player = pl.transform;

            Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y).normalized;
            toPlayer += (new Vector2(Vector2.Perpendicular(toPlayer).x * shift, Vector2.Perpendicular(toPlayer).y * shift));
            rgbd.velocity = Speed * toPlayer;
        }
        private float GetRandomShift()
        {
            return UnityEngine.Random.Range(-0.6f, 0.6f);
        }
    }
}
