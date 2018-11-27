using Assets.Scripts;
using UnityEngine;

public class SineRatProjectile : RatProjectile
{
    private float sineShift = 0;

    public override void ShootUntargeted()
    {
        sineShift = GetSineShift();
        Vector2 toPlayer = new Vector2(sineShift, (-1));
        rgbd.velocity = Speed * toPlayer;
    }  
    public override void ShootTargeted()
    {
        sineShift = GetSineShift();
        Transform player = GameObject.Find("Player").transform;
        Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y).normalized;
        toPlayer += (new Vector2(Vector2.Perpendicular(toPlayer).x * sineShift, Vector2.Perpendicular(toPlayer).y * sineShift));
        rgbd.velocity = Speed * toPlayer;
    }
    private float GetSineShift()
    {
        return (Mathf.Sin(4*Time.time) / Mathf.PI);
    }
}
