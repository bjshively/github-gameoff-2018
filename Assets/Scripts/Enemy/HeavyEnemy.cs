using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy {

    public GameObject smashCollider;
    bool attackMode = false;
    bool retreatMode = true;
    bool sleepMode = false;
    float retreatTime = 3;
    float sleepTime = 1.5f;

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
        Move();
    }

    void MoveToLocation(Vector3 pos)
    {
        // The step size is equal to speed times frame time.
        float step = MoveSpeed.Value * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos, step);
        transform.LookAt(pos);
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
            // Target player
            anim.SetFloat("MoveSpeed", MoveSpeed.Value);
            

            // Attack if within range
            if (targetDistance <= 3)
            {
                Attack();
            } else
            {
                MoveToLocation(target);
            }

        } else if (retreatMode)
        {
            // Switch to sleep mode if time is up
            retreatTime -= Time.deltaTime;
            if(retreatTime <= 0)
            {
                StartSleeping();
            }

            if (targetDistance >= 2)
            {
                MoveToLocation(target);
            }
            else
            {
                anim.SetFloat("MoveSpeed", 0);
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
            }
            Debug.Log("Sleepmode");
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
        retreatTime = 3;
        target = new Vector3(playerTransform.Value.position.x + Random.Range(-50, 50), transform.position.y, playerTransform.Value.position.z + Random.Range(-5, 5));
        retreatMode = true;
    }

    // Triggered after retreat
    void StartSleeping()
    {
        sleepTime = 1.5f;
        anim.SetFloat("MoveSpeed", 0);
        canAttack = true;
        retreatMode = false;
        sleepMode = true;
        attackMode = false;
    }
}