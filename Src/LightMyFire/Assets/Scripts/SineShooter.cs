using Assets.Scripts;
using UnityEngine;

public class SineShooter : Shooter
{
    public float Speed;
    // are we targeting the player?
    private float sineShift = 0;

    public override void ShootUntargeted()
    {
        sineShift = GetSineShift();
        rgbd.velocity = new Vector2(Speed * sineShift, Speed * (-1)); 
    }  
    public override void ShootTargeted()
    {
        sineShift = GetSineShift();
        Transform player = GameObject.Find("dummyPlayer").transform;
        Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        toPlayer += (new Vector2(Vector2.Perpendicular(toPlayer).x * sineShift, Vector2.Perpendicular(toPlayer).y * sineShift));
        rgbd.velocity = Speed * toPlayer;
    }
    private float GetSineShift()
    {
        return (Mathf.Sin(4*Time.time) / Mathf.PI);
    }
}
