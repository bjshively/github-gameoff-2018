using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy {

    public GameObject smashCollider;
    public bool attackMode = false;
    public bool retreatMode = true;
    public bool sleepMode = false;
    float retreatTime = 3;
    float sleepTime = 1.5f;
    float attackTimer = 5f;

    Vector3 target;
    float targetDistance;
    bool canAttack = true;

    // Use this for initialization
    protected override void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isAlive = true;
        StartRetreat();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if (playerHealth.Value > 0)
        {
            if (isAwake)
            {
                    Move();
            }
            else
            {
                anim.SetFloat("MoveSpeed", 0);
            }
        }
    }

    void MoveToLocation(Vector3 pos)
    {
        if (canMove)
        {
            anim.SetFloat("MoveSpeed", MoveSpeed.Value);
            // The step size is equal to speed times frame time.
            float step = MoveSpeed.Value * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pos, step);
            transform.LookAt(pos);
        }
    }

    protected override void Move()
    {
        if (!attackMode && !sleepMode && !retreatMode)
        {
            attackMode = true;
        }

        targetDistance = Mathf.Abs(Vector3.Distance(target, transform.position));

        if (attackMode)
        {
            attackTimer -= Time.deltaTime;
            // Attack if within range
            if (targetDistance <= 3)
            {
                anim.SetFloat("MoveSpeed", 0);
                Attack();
            } else
            {
                MoveToLocation(target);
            }
            if(attackTimer <= 0)
            {
                StartRetreat();
            }

        } else if (retreatMode)
        {
            attackTimer = 5f;
            anim.SetFloat("Retreating", 1);
            // Switch to sleep mode if time is up or retreat location has been reached
            retreatTime -= Time.deltaTime;
            if(retreatTime <= 0 || targetDistance < 2)
            {
                anim.SetFloat("MoveSpeed", 0);
                StartSleeping();
            } else {    
                MoveToLocation(target);
            }

        } else if (sleepMode)
        {
            // Switch back to attack mode if time is up
            sleepTime -= Time.deltaTime;
            if(sleepTime <= 0)
            {
                attackMode = true;
                sleepMode = false;
                target = playerTransform.Value.position;
            } else
            {
                anim.SetFloat("MoveSpeed", 0);
            }
        }
    }

    override protected void Attack()
    {
        if (canAttack)
        {
            anim.SetTrigger("Combo3Heavy");
        }
        canAttack = false;
        attackMode = false;
    }

    // Triggered in animation controller
    void StartRetreat()
    {
        anim.SetFloat("Retreating", 1);
        attackMode = false;
        sleepMode = false;
        retreatMode = true;
        retreatTime = 3;
        target = new Vector3(Mathf.Clamp(playerTransform.Value.position.x + Random.Range(-50, 50), 0, Mathf.Infinity), transform.position.y, Random.Range(-20, 4));

    }

    // Triggered after retreat
    void StartSleeping()
    {
        anim.SetFloat("Retreating", 0);
        transform.LookAt(playerTransform.Value.position);
        sleepTime = 1.5f;
        anim.SetFloat("MoveSpeed", 0);
        canAttack = true;
        retreatMode = false;
        sleepMode = true;
        attackMode = false;
    }
}