using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public GameObject gunBarrel;
    bool attackMode = false;
    bool retreatMode = true;
    bool sleepMode = false;
    float retreatTime = 3;
    float sleepTime = 1.5f;
    float attackTime = 3;


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
        if (isAwake)
        {
            Move();
        }
        else
        {
            anim.SetFloat("MoveSpeed", 0);
        }
    }

    void MoveToLocation(Vector3 pos)
    {
        anim.SetFloat("MoveSpeed", MoveSpeed.Value);
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
            attackTime -= Time.deltaTime;
            // Attack if within range
            if (targetDistance <= 3 || attackTime <= 0)
            {
                anim.SetFloat("MoveSpeed", 0);
                Attack();
            }
            else
            {
                MoveToLocation(target);
            }

        }
        else if (retreatMode)
        {
            // Switch to sleep mode if time is up
            retreatTime -= Time.deltaTime;
            if (retreatTime <= 0)
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
        }
        else if (sleepMode)
        {
            // Switch back to attack mode if time is up
            sleepTime -= Time.deltaTime;
            if (sleepTime <= 0)
            {
                attackMode = true;
                attackTime = 3;
                sleepMode = false;
                target = target = new Vector3(playerTransform.Value.position.x + Random.Range(-35, 35), transform.position.y, playerTransform.Value.position.z + Random.Range(-5, 5));
            }
            else
            {
                anim.SetFloat("MoveSpeed", 0);
            }
        }
    }

    override protected void Attack()
    {
        if (canAttack)
        {
            Vector3 barrelPoint = gunBarrel.transform.position;
            GameObject bullet = Instantiate(Resources.Load("Bullet") as GameObject);
            bullet.transform.position = barrelPoint;
        }
        canAttack = false;
        attackMode = false;
        StartRetreat();
    }

    // Triggered in animation controller
    void StartRetreat()
    {
        retreatTime = 3;
        target = new Vector3(playerTransform.Value.position.x + Random.Range(-35, 35), transform.position.y, playerTransform.Value.position.z + Random.Range(-5, 5));
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