﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRatProjectile :  RatProjectile
{
    public override void ShootUntargeted()
    {
        Vector2 toPlayer = new Vector2(0, -1);
        rgbd.velocity = Speed * toPlayer;
    }
    public override void ShootTargeted()
    {
        GameObject pl = GameObject.Find("Vajgl");
        // better safe than sorry
        if (pl == null) return;
        Transform player = pl.transform;
        
        Vector2 toPlayer = (new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y))).normalized;
        rgbd.velocity = Speed * toPlayer;
    }  
}
