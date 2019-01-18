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

    // melee attacks
    [SerializeField] public GameObject ShortRangeAttack;
    [SerializeField] public GameObject LongRangeAttack;
    [SerializeField] public Transform ShortRangeAttackSpawn;
    [SerializeField] public Transform LongRangeAttackSpawn;

    // rat itself
    [SerializeField] [Range(0, .3f)] private float movementSmoothing = .05f;
    [SerializeField] private float nextRunStart = 0;
    [SerializeField] private float runEnd = 0;
    [SerializeField] private float runningDuration = 2.0f;
    [SerializeField] private float idleDuration = 3f;
    private Rigidbody2D rgbd;
    private Vector3 velocity = Vector3.zero;
    private Vector3 curDir;
    private Vector3 lastDir;
    private Animator animator;
    private bool running = false;

    # region Phase two and movement
    public void TransformToPhaseTwo()
    {
        if (phaseTwo) return;
        phaseTwo = true;

        // rat falls down
        gameObject.transform.rotation = Quaternion.identity;
        if (curDir.x < 0) { gameObject.transform.Rotate(0f, 180f, 0f); }
        //CheckOrientation();

        // spawns are now the empty ends of the main pipe
        ShotSpawn1 = new Vector3(3.8f, 7.2f, 0);
        ShotSpawn2 = new Vector3(-13f, 1f, 0);
        scheduler = new AttackScheduler(0f, 2);
        shotController.fireRate = 0.4f;

        nextRunStart = Time.time + idleDuration;
    }
    private void CheckOrientation()
    {
        GameObject pl = GameObject.Find("Vajgl");
        // better safe than sorry
        if (pl == null) return;
        Transform player = pl.transform;

        Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, 0);
        float move = toPlayer.x;

        if ((move < 0 && !isFacingLeft) || (move > 0 && isFacingLeft)) { TurnRound(); }
    }
    private void Move()
    {
        // do not move when attacking
        if (attackController.IsAttacking()) return;

        // end any attack animations
        EndAttackAnimations();
        CheckOrientation();
        Pursuit();
    }   
    private void Pursuit()
    {
        // running -> running
        if (Time.time < runEnd)
        {
            GameObject pl = GameObject.Find("Vajgl");
            // better safe than sorry
            if (pl == null) return;
            Transform player = pl.transform;

            Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, 0);
            float move = toPlayer.normalized.x;
            if ((toPlayer.sqrMagnitude <= 4.0f))
            {
                Stop();
                return;
            }
            animator.SetFloat("Speed", Mathf.Abs(move));
            Vector3 targetVelocity = new Vector2(move * 6f, rgbd.velocity.y);
            rgbd.velocity = Vector3.SmoothDamp(rgbd.velocity, targetVelocity, ref velocity, movementSmoothing);
        }
        // idle -> running
        else if (Time.time > nextRunStart && nextRunStart > 0)
        {
            runEnd = Time.time + runningDuration;
            nextRunStart = 0;
            running = true;
        }
        // running -> idle
        else if (Time.time >= runEnd && runEnd > 0)
        {
            Stop();
        }
    }
    private void WanderAround()
    {       
        // running -> running
        if (Time.time < runEnd)
        {            
            float move = curDir.normalized.x;
            if ((move < 0 && !isFacingLeft) || (move > 0 && isFacingLeft)) { TurnRound(); }
            animator.SetFloat("Speed", Mathf.Abs(move));
            Vector3 targetVelocity = new Vector2(curDir.x*0.2f, rgbd.velocity.y);
            rgbd.velocity = Vector3.SmoothDamp(rgbd.velocity, targetVelocity, ref velocity, movementSmoothing);
        }
        // idle -> running
        else if (Time.time > nextRunStart && nextRunStart > 0)
        {
            runEnd = Time.time + runningDuration;
            nextRunStart = 0;
            running = true;
            if (lastDir == ShotSpawn3)
            {
                curDir = ShotSpawn1 - transform.position;
                lastDir = ShotSpawn1;
            }
            else
            {
                curDir = ShotSpawn3 - transform.position;
                lastDir = ShotSpawn3;
            }
        }
        // running -> idle
        else if (Time.time >= runEnd && runEnd > 0)
        {
            Stop();
        }
    }
    public void Stop()
    {
        nextRunStart = Time.time + idleDuration;
        runEnd = 0;
        animator.SetFloat("Speed", 0);
        running = false;
    }
    private void TurnRound()
    {
        isFacingLeft = !isFacingLeft;
        gameObject.transform.Translate(-3, 0, 0);
        gameObject.transform.Rotate(0f, 180f, 0f);
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
        if (Time.time > nextShooting)
        {
            nextShooting = Time.time + fireRate;
            var config = scheduler.ScheduleShooter();
            SetNewShooter(config);
        }
    }
    private void CheckAttacking()
    {
        CheckShortRangedAttack();
        if (!running) CheckLongRangedAttack();
    }
    private void CheckShortRangedAttack()
    {
        if (attackController.IsAttacking()) return;
        
        GameObject pl = GameObject.Find("Vajgl");
        // better safe than sorry
        if (pl == null) return;
        Transform player = pl.transform;

        Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        if (toPlayer.sqrMagnitude <= 16.0f)
        {
            EndAttackAnimations();
            Stop();
            animator.SetBool("IsShortRangeAttacking", true);
            attackController.SetNewAttack(ShortRangeAttack, ShortRangeAttackSpawn.position, scheduler.ScheduleAttackOpening(0f));
        }
    }
    private void CheckLongRangedAttack()
    {
        if (attackController.IsAttacking()) return;

        EndAttackAnimations();
        animator.SetBool("IsLongRangeAttacking", true);
        attackController.SetNewAttack(LongRangeAttack, LongRangeAttackSpawn.position, scheduler.ScheduleAttackOpening(0f));       
    }
    private void EndAttackAnimations()
    {
        animator.SetBool("IsShortRangeAttacking", false);
        animator.SetBool("IsLongRangeAttacking", false);
    }
    #endregion

    #region Unity engine called methods
    void Start()
    {
        // difficulty!! 0 by default
        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scheduler = new AttackScheduler(0f, 3);

        nextRunStart = Time.time + idleDuration;
        ShortRangeAttack.transform.localScale = new Vector3(2, 2, 0);
        LongRangeAttack.transform.localScale = new Vector3(2.8f, 2, 0);
    }
	void FixedUpdate ()
    {
        // always shoot
        CheckShooting();
        shotController.Shoot();

        if (phaseTwo)
        {
            CheckAttacking();
            attackController.Attack();
            Move();
        }
        else
        {
            WanderAround();
        }
    }
    #endregion 
}
