using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShooter : Shooter
{
    public float Speed;
    // are we targeting the player?

    public override void ShootUntargeted()
    {
        float shift = GetRandomShift();
        rgbd.velocity = new Vector2(Speed * shift, Speed * (-1));
    }
    public override void ShootTargeted()
    {
        Transform player = GameObject.Find("dummyPlayer").transform;
        float shift = GetRandomShift();
        Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        toPlayer += (new Vector2(Vector2.Perpendicular(toPlayer).x * shift, Vector2.Perpendicular(toPlayer).y * shift));
        rgbd.velocity = Speed * toPlayer;
    }
    private float GetRandomShift()
    {
        return Random.Range(-0.4f, 0.4f);
    }
}
