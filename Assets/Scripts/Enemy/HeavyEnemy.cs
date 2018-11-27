using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy {

    public GameObject smashCollider;

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    protected override void Move()
    {
        // Calculate the distance to the player
        playerDistance = Mathf.Abs(Vector3.Distance(playerTransform.Value.position, transform.position));

        // Recalculate trajectory when recalculation limit as lapsed, or when the target location has been reached (prevent walking in place)
        if ((time % movementRecalculation == 0) || Vector3.Distance(transform.position, playerDirection) < 1)
        {
            // Set a new trajectory towards the player
            playerDirection = new Vector3(playerTransform.Value.position.x, transform.position.y, playerTransform.Value.position.z);
        }

        // If player in sight, but not in attack range, move towards player
        if (playerDistance < playerInSight.Value && playerDistance >= playerInRange && canMove)
        {
            anim.SetFloat("MoveSpeed", MoveSpeed.Value);
            // The step size is equal to speed times frame time.
            float step = MoveSpeed.Value * Time.deltaTime;

            // Move position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, playerDirection, step);

            // Rotate to face player
            transform.LookAt(playerDirection);
        }
        else
        {
            anim.SetFloat("MoveSpeed", 0);

            // When close to the player, aim precisely
            if (playerDistance < playerInRange)
            {
                playerDirection = new Vector3(playerTransform.Value.position.x, transform.position.y, playerTransform.Value.position.z);
            }
            if (canMove)
            {
                // Rotate to face player
                transform.LookAt(playerDirection);
            }
        }

        // Only attack if the player is within attack range
        if (playerDistance < playerInRange)
        {
            if (Random.Range(0, 200) == 1)
            {
                Attack();
            }
        }
    }

    override protected void Attack()
    {
        anim.SetTrigger("Combo3");
    }
}
