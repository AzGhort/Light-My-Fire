using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttacker : Attacker
{
    void Start()
    {
        attackTime = 2.0f;
    }

    public override void DoDamage()
    {
        // TO DO 
        //Collider2D[] hittedObjects = Physics2D.OverlapBox(...)
        //for (int i = 0; i < hittedObjects.Length; i++) ...
    }
}
