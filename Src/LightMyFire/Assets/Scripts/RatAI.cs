using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAI : MonoBehaviour
{
    // shots and shot spawns
    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public Transform shotSpawn1;
    public Transform shotSpawn2;
    public Transform shotSpawn3;
    private ShotController shotController = new ShotController();
    private AttackScheduler scheduler;
    private bool phaseTwo = false;
    private float nextShooting = 0;
    private float fireRate = 3.0f;
    // melee attacks
    public GameObject attack1;
    public GameObject attack2;

    public void TransformToPhaseTwo()
    {
        phaseTwo = true;
        // create rat somewhere
    }

    private void SetNewShooter(ShotConfiguration config)
    {
        Transform shotspawn = null;
        GameObject shot = null;
        Shooter shooter = null;
        switch (config.Type)
        {
            case ShotType.RANDOM_UNTARGETED:
                shot = shot3;
                break;
            case ShotType.SINE_UNTARGETED:
                shot = shot2;
                break;
            case ShotType.STRAIGHT_UNTARGETED:
                shot = shot1;
                break;
            case ShotType.RANDOM_TARGETED:
                shot = shot3;
                break;
            case ShotType.SINE_TARGETED:
                shot = shot2;
                break;
            case ShotType.STRAIGHT_TARGETED:
                shot = shot1;
                break;
            default:
                shot = shot1;
                break;
        }
        switch (config.Spawn)
        {
            case 0:
                shotspawn = shotSpawn1;
                break;
            case 1:
                shotspawn = shotSpawn2;
                break;
            case 2:
                shotspawn = shotSpawn3;
                break;
            default:
                shotspawn = shotSpawn1;
                break;
        }
        shooter = shot.GetComponent<Shooter>();
        if (config.Type == ShotType.RANDOM_UNTARGETED || config.Type == ShotType.SINE_UNTARGETED 
            || config.Type == ShotType.STRAIGHT_UNTARGETED)
        {
            shooter.Target = false;
        }
        else
        {
            shooter.Target = true;
        }
        shotController.AddShooter(shooter, shot, shotspawn, config.Duration);
    }
    private void CheckShooting()
    {
        // times in between shootings decrease over time
        if (fireRate > 1.5f) fireRate -= 0.015f;
        if (Time.time > nextShooting)
        {
            nextShooting = Time.time + fireRate;
            var config = scheduler.ScheduleShooter();
            SetNewShooter(config);
        }
    }
    private void CheckAttacking()
    {

    }

    #region Unity engine called methods
    void Start()
    {
        scheduler = new AttackScheduler(0f, 3);
    }
	void FixedUpdate ()
    {
        // always shoot
        CheckShooting();
        shotController.Shoot();
        if (phaseTwo)
        {
            CheckAttacking();
        }
    }
    #endregion 
}
