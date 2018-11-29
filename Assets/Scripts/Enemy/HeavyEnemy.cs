using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy {

    public GameObject smashCollider;
    public bool attacking = true;
    public bool sleeping = false;
    public bool retreating = false;
    Vector3 retreatPosition;
    float sleepTime = 5;
    Vector3 target = Vector3.zero;
    float targetDistance;

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    void MoveToLocation(Vector3 pos)
    {
        // The step size is equal to speed times frame time.
        float step = MoveSpeed.Value * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos, step);
        transform.LookAt(pos);
    }

    protected override void Move()
    {
        // Target the player only once per movement round
        if(target == Vector3.zero)
        {
            target = playerTransform.Value.position;
        }

        if (attacking)
        {
            // Target player
            targetDistance = Mathf.Abs(Vector3.Distance(target, transform.position));
            target = new Vector3(playerTransform.Value.position.x, transform.position.y, playerTransform.Value.position.z);
            anim.SetFloat("MoveSpeed", MoveSpeed.Value);

            // Run to player
            

            if(targetDistance <= 3)
            {
                Attack();
            } else {
                MoveToLocation(target);
            }

        } else if (retreating)
        {
            MoveToLocation(retreatPosition);
            if(Vector3.Distance(retreatPosition, transform.position) <= 3)
            {
                retreating = false;
                sleeping = true;
            }

        } else if(sleeping)
        {
            // Do sleep stuff
            StartCoroutine(StartSleeping());
        }
    }

    override protected void Attack()
    {
        attacking = false;
        //StartCoroutine(StartRetreat());
        anim.SetTrigger("Combo3Heavy");
    }

    IEnumerator StartRetreat()
    {
        retreatPosition = new Vector3(playerTransform.Value.position.x + Random.Range(-10, 10), transform.position.y, playerTransform.Value.position.z + Random.Range(-10, 10));
        yield return new WaitForSeconds(.5f);
        retreating = true;
        sleepTime = 5;
    }

    IEnumerator StartSleeping()
    {
        anim.SetFloat("MoveSpeed", 0);
        yield return new WaitForSeconds(sleepTime);

        // Zero out target so player is retargeted
        // Transition from sleep to attack mode
        target = Vector3.zero;
        sleeping = false;
        attacking = true;
    }
}