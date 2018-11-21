using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShooter :  Shooter
{
    public float Speed;
    // are we targeting the player?

    public override void ShootUntargeted()
    {
        rgbd.velocity = new Vector2(0, Speed*(-1));
    }
    public override void ShootTargeted()
    {
        Transform player = GameObject.Find("dummyPlayer").transform;
        rgbd.velocity = new Vector2(Speed*(player.position.x - transform.position.x), Speed*(player.position.y - transform.position.y));
    }  
}
