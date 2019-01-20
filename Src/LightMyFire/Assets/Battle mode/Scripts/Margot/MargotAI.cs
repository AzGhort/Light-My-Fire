using Assets.Scripts;
using LightMyFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MargotAI : MonoBehaviour
{        
    [SerializeField] private float nextAttackStart = 0;
    [SerializeField] private float attackRate = 7.0f;
    [SerializeField] private float attackDuration = 4.5f;
    [SerializeField] private float shootingDuration = 3f;
    [SerializeField] private float attackEnd = 0;
    [SerializeField] private float damage = 0.5f;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isShooting = false;

    // shooting
    private ShootingController shotController = new ShootingController();
    [SerializeField] public GameObject MargotMainShot;
    [SerializeField] public GameObject MargotAdjacentShot;

    // margot itself
    [SerializeField] [Range(0, .3f)] private float movementSmoothing = .05f;
    [SerializeField] private float nextRunStart = 0;
    [SerializeField] private float runEnd = 0;
    [SerializeField] private float runningDuration = 3.0f;
    [SerializeField] private float idleDuration = 1f;
    [SerializeField] private bool isFacingLeft = false;

    private Rigidbody2D rgbd;
    private Collider2D Collider;
    private Vector3 velocity = Vector3.zero;
    private Animator animator;
    //private bool isRunning = false;

    # region Movement
    private void CheckOrientation()
    {
        Transform player = GameObject.Find("Vajgl").transform;
        Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, 0);
        float move = toPlayer.x;

        if ((move < 0 && !isFacingLeft) || (move > 0 && isFacingLeft)) { TurnRound(); }
    }
    private void Move()
    {
        // do not move when attacking
        if (isAttacking || isShooting) return;

        if (Time.time > nextRunStart)
        {
            runEnd = Time.time + runningDuration;
            //isRunning = true;
            nextRunStart = float.MaxValue;
            CheckOrientation();
            Pursuit();
        }
        else if (Time.time <= runEnd)
        {
            CheckOrientation();
            Pursuit();
        }
        else if (Time.time > runEnd)
        {
            Stop();
        }
    }
    private void Pursuit()
    {
        // safety first
        var pl = GameObject.Find("Vajgl");
        Transform player;
        if (pl) { player = pl.transform; }
        else { return; }

        Vector2 toPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        float moveX = toPlayer.normalized.x;
        float moveY = toPlayer.normalized.y;
        if ((toPlayer.sqrMagnitude <= 4.0f))
        {
            Stop();
            return;
        }
        Vector3 targetVelocity = new Vector2(moveX * 6f, moveY * 6f);
        rgbd.velocity = Vector3.SmoothDamp(rgbd.velocity, targetVelocity, ref velocity, movementSmoothing);
    } 
    private void Stop()
    {
        //isRunning = false;
        gameObject.transform.rotation = Quaternion.identity;
        nextRunStart = Time.time + idleDuration;
        rgbd.velocity = Vector3.zero;
        runEnd = float.MaxValue;
    }
    private void TurnRound()
    {
        isFacingLeft = !isFacingLeft;
        gameObject.transform.Rotate(0f, 180f, 0f);
    }
    #endregion

    #region Attacks handling 
    private void SetAttackRate()
    {
        float safeBound = ((7f - (Time.time / 50f)) > 2.5f) ? 7f - (Time.time / 50f) : 2.5f;
        attackRate = Random.Range(2f, safeBound);
    }
    private void StopAttacking()
    {
        isAttacking = false;
        EndAttackAnimations();
        //SetAttackRate();
        nextAttackStart = Time.time + attackRate;
        rgbd.velocity = Vector3.zero;
        attackEnd = float.MaxValue;
        rgbd.gravityScale = 1;
    }
    private void CheckAttacking()
    {
        if (Time.time > nextAttackStart)
        {
            //CheckOrientation();
            ChooseNewAttack();
        }
        if (Time.time > attackEnd)
        {
            StopAttacking();
        }
    }
    private void ChooseNewAttack()
    {
        var pl = GameObject.Find("Vajgl");
        Vector3 player;
        if (pl) { player = pl.transform.position; }
        else { return; }
        nextAttackStart = float.MaxValue;
        Vector2 toPlayer = new Vector2(player.x - transform.position.x, player.y - transform.position.y);
        
        float f = Random.Range(0f, 1f);
        bool above = IsAbovePlatform();
        if (f >= 0.4f && !above && toPlayer.sqrMagnitude > 10f)
        {
            attackEnd = Time.time + shootingDuration;
            rgbd.gravityScale = 0;
            Stop();
            StartShooting();            
        }
        else
        {
            attackEnd = Time.time + attackDuration;
            SquidAttack();
        }
    }
    private void StartShooting()
    {
        isShooting = true;
        //isRunning = false;
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsShooting", true);
        animator.SetBool("IsFlying", false);        
        rgbd.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        shotController.Reset();
        shotController.AddShooter(MargotMainShot, gameObject.transform.position, shootingDuration);
        shotController.AddShooter(MargotAdjacentShot, gameObject.transform.position, shootingDuration);
    }
    private bool IsAbovePlatform()
    {
        bool above = false;
        Vector2 position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        var platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (var platform in platforms)
        {
            var collider = platform.GetComponent<Collider2D>();
            if (collider)
            {
                if (collider.OverlapPoint(position)) { above = true; }
            }
        }
        return above;
    }
    private void RotateToPlayer()
    {
        var pl = GameObject.Find("Vajgl");
        Vector3 player;
        if (pl) { player = pl.transform.position; }
        else { return; }

        Vector3 toPlayer = new Vector3(player.x - transform.position.x, player.y - transform.position.y, 0);
        Quaternion q = new Quaternion();
        q.SetFromToRotation(Vector3.down, toPlayer);
        gameObject.transform.rotation = q;
    }
    private void ContinueShooting()
    {
        RotateToPlayer();
        shotController.Shoot();
    }
    private void SquidAttack()
    {
        isShooting = false;
        //isRunning = false;
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsFlying", false);

        var pl = GameObject.Find("Vajgl");
        Vector3 player;
        if (pl) { player = pl.transform.position; }
        else { return; }

        Vector3 attackTarget = player + new Vector3(0, 5, 0);
        Vector2 toPlayer = new Vector2(attackTarget.x - transform.position.x, attackTarget.y - transform.position.y);
        float moveX = toPlayer.normalized.x;
        float moveY = toPlayer.normalized.y;
        float magnitude = toPlayer.sqrMagnitude;
        if (magnitude <= 6.0f)
        {
            //Vector2 tp = new Vector2(player.x - transform.position.x, player.y - transform.position.y);
            float mX = toPlayer.normalized.x;
            float mY = toPlayer.normalized.y;
            Vector3 vel = new Vector2(mX * 9f, mY * 9f);
            rgbd.velocity = Vector3.SmoothDamp(rgbd.velocity, vel, ref velocity, movementSmoothing);
            return;
        }
        Vector3 targetVelocity = new Vector2(moveX * 6f, moveY * 6f);
        rgbd.velocity = Vector3.SmoothDamp(rgbd.velocity, targetVelocity, ref velocity, movementSmoothing);
    }
    private void EndAttackAnimations()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsFlying", true);
        transform.rotation = Quaternion.identity;
        isAttacking = false;
        isShooting = false;
    }
    #endregion

    #region Unity engine called methods
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        var colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            if (!col.isTrigger)
            {
                Collider = col;
            }
        }
        animator = GetComponent<Animator>();
        SetAttackRate();
        shotController.fireRate = 0.3f;
        nextAttackStart = Time.time + attackRate;
    }
    void FixedUpdate()
    {
        Move();
        CheckAttacking();
        if (isAttacking) { SquidAttack(); }
        else if (isShooting) { ContinueShooting(); }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(Collider, collision.collider);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.GetComponent<Collider2D>().GetComponent<PlayerHealthManager>();
            if (player) { player.TakeDamage(damage); }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.GetComponent<Collider2D>().GetComponent<PlayerHealthManager>();
            if (player) { player.TakeDamage(damage); }
        }
    }
    #endregion 
}
