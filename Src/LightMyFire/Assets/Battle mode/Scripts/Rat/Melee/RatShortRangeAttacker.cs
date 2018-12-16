using Assets.Scripts;
using LightMyFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatShortRangeAttacker : RatAttacker
{
    public override void DoDamage()
    {
        coll.gameObject.SetActive(true);
        sprnd.gameObject.SetActive(true);
    }
    public override void Setup()
    {
       attackTime = 3f;
       coll = gameObject.GetComponent<Collider2D>();
       coll.gameObject.SetActive(false);
       sprnd = gameObject.GetComponent<SpriteRenderer>();
       sprnd.gameObject.SetActive(false);
    }
}
