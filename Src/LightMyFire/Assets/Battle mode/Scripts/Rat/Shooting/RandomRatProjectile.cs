using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRatProjectile : RatProjectile
{
    public override void ShootUntargeted()
    {
        float shift = GetRandomShift();
        Vector2 toPlayer = new Vector2(shift, (-1));
        rgbd.velocity = Speed * toPlayer;
    }
    public override void ShootTargeted()
    {
        float shift = GetRandomShift();
        Transform player = GameObject.Find("Vajgl").transform;        
        Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y).normalized;
        toPlayer += (new Vector2(Vector2.Perpendicular(toPlayer).x * shift, Vector2.Perpendicular(toPlayer).y * shift));
        rgbd.velocity = Speed * toPlayer;
    }
    private float GetRandomShift()
    {
        return Random.Range(-0.4f, 0.4f);
    }
}
