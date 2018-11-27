using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAI : MonoBehaviour
{
    // shots and shot spawns
    [SerializeField] public GameObject StraightShot;
    [SerializeField] public GameObject SineShot;
    [SerializeField] public GameObject RandomShot;
    [SerializeField] private Vector3 ShotSpawn1;
    [SerializeField] private Vector3 ShotSpawn2;
    [SerializeField] private Vector3 ShotSpawn3;


    // controllers and schedulers
    private ShootingController shotController = new ShootingController();
    private MeleeController attackController = new MeleeController();
    private AttackScheduler scheduler;

    // shooting
    [SerializeField] private bool phaseTwo = false;
    [SerializeField] private bool isFacingLeft = false;
    [SerializeField] private float nextShooting = 0;
    [SerializeField] private float fireRate = 3.0f;
    [SerializeField] private float nextAttack = 0;
    [SerializeField] private float attackRate = 5.0f;

    // melee attacks
    [SerializeField] public GameObject ShortRangedAttack;
    [SerializeField] public GameObject LongRangeAttack;
    [SerializeField] public Transform ShortRangeAttackSpawn;
    [SerializeField] public Transform LongRangeAttackSpawn;

    // rat itself
    [SerializeField] public Transform RatSpawn;
    [SerializeField] [Range(0, .3f)] private float movementSmoothing = .05f;
    [SerializeField] private float nextRunStart = 0;
    [SerializeField] private float runEnd = 0;
    [SerializeField] private float runningDuration = 2.0f;
    [SerializeField] private float idleDuration = 1.5f;
    private Rigidbody2D rgbd;
    private Vector3 velocity = Vector3.zero;
    private Animator animator;

    # region Phase two and movement
    private void TransformToPhaseTwo()
    {
        phaseTwo = true;
        // shift rat + animation?
        transform.Translate(RatSpawn.position.x, 0, 0);
        nextRunStart = Time.time + idleDuration;
    }
    private void Move()
    {
        // do not move when attacking
        if (attackController.IsAttacking()) return;

        Transform player = GameObject.Find("Player").transform;
        Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, 0);
        float move = toPlayer.normalized.x;

        if ((move < 0 && !isFacingLeft) || (move > 0 && isFacingLeft)) { TurnRound(); }
        Run(toPlayer, move);
    }   
    private void Run(Vector2 toPlayer, float move)
    {
        // running -> running
        if (Time.time < runEnd)
        {
            if ((toPlayer.sqrMagnitude <= 4.0f))
            {
                Stop();
                return;
            }
            animator.SetFloat("Speed", Mathf.Abs(move));
            Vector3 targetVelocity = new Vector2(move * 3f, rgbd.velocity.y);
            rgbd.velocity = Vector3.SmoothDamp(rgbd.velocity, targetVelocity, ref velocity, movementSmoothing);
        }
        // idle -> running
        else if (Time.time > nextRunStart && nextRunStart > 0)
        {
            runEnd = Time.time + runningDuration;
            nextRunStart = 0;
        }
        // running -> idle
        else if (Time.time >= runEnd && runEnd > 0)
        {
            Stop();
        }
    }
    private void Stop()
    {
        nextRunStart = Time.time + idleDuration;
        runEnd = 0;
        animator.SetFloat("Speed", 0);
    }
    private void TurnRound()
    {
        isFacingLeft = !isFacingLeft;
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion

    #region Attacks handling

    private void SetNewShooter(ShotConfiguration config)
    {
        // no shooting was scheduled - all shooters full
        if (config == null) return;

        Vector3 shotspawn;
        GameObject shot = null;
        RatProjectile shooter = null;
        switch (config.Type)
        {
            case ShotType.RANDOM_UNTARGETED:
                shot = RandomShot;
                break;
            case ShotType.SINE_UNTARGETED:
                shot = SineShot;
                break;
            case ShotType.STRAIGHT_UNTARGETED:
                shot = StraightShot;
                break;
            case ShotType.RANDOM_TARGETED:
                shot = RandomShot;
                break;
            case ShotType.SINE_TARGETED:
                shot = SineShot;
                break;
            case ShotType.STRAIGHT_TARGETED:
                shot = StraightShot;
                break;
            default:
                shot = StraightShot;
                break;
        }
        switch (config.Spawn)
        {
            case 0:
                shotspawn = ShotSpawn1;
                break;
            case 1:
                shotspawn = ShotSpawn2;
                break;
            case 2:
                shotspawn = ShotSpawn3;
                break;
            default:
                shotspawn = ShotSpawn1;
                break;
        }
        shooter = shot.GetComponent<RatProjectile>();
        if (config.Type == ShotType.RANDOM_UNTARGETED || config.Type == ShotType.SINE_UNTARGETED 
            || config.Type == ShotType.STRAIGHT_UNTARGETED)
        {
            shooter.Target = false;
        }
        else
        {
            shooter.Target = true;
        }
        shotController.AddShooter(shot, shotspawn, config.Duration);
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
        if (attackController.IsAttacking()) return;
        else
        {
            if (attackRate > 0.5f) attackRate -= 0.015f;
            Transform player = GameObject.Find("Player").transform;
            Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
            // player is too close -> always attack
            if (toPlayer.sqrMagnitude <= 3.0f)
            {
                Stop();
                attackController.SetNewAttack(ShortRangedAttack, ShortRangeAttackSpawn, scheduler.ScheduleAttackOpening(0.5f));
            }
            // player is far AND we are not moving AND it's time for attack
            else if (Time.time > nextAttack && nextRunStart > 0)
            {
                nextAttack = Time.time + attackRate;
                attackController.SetNewAttack(LongRangeAttack, LongRangeAttackSpawn, scheduler.ScheduleAttackOpening(0.7f));
            }
        }
    }
    #endregion

    #region Unity engine called methods
    void Start()
    {
        // difficulty!! 0 by default
        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scheduler = new AttackScheduler(0f, 3);
    }
	void FixedUpdate ()
    {
        // always shoot
        CheckShooting();
        shotController.Shoot();

        // TO-DO
        // RAT IS NOW TRANSFORMING AUTOMATICALLY AFTER 10 SECONDS
        if (Time.time > 10f && !phaseTwo) { TransformToPhaseTwo(); }

        if (phaseTwo)
        {
            Move();
            CheckAttacking();
            attackController.Attack();
        }
    }
    #endregion 
}
