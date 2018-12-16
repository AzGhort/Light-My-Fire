using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatLongRangeAttacker : RatAttacker
{
    public override void DoDamage()
    {
        coll.gameObject.SetActive(true);
        sprnd.gameObject.SetActive(true);
    }

    public override void Setup()
    {
        attackTime = 4.0f;
        coll = gameObject.GetComponent<Collider2D>();
        coll.gameObject.SetActive(false);
        sprnd = gameObject.GetComponent<SpriteRenderer>();
        sprnd.gameObject.SetActive(false);
    }
}
