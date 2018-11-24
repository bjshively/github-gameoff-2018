using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Animator anim;
    private Rigidbody body;
    public GameObject attackCollider;
    public TransformVariable playerTransform;
    public FloatReference enemyHitTime;
    public FloatReference alertness;
    float playerDistance;
    public FloatReference MoveSpeed;
    private bool canMove = true;
    public bool isInvincible = false;
    public int health;
    float time;

    // Pick a random number of seconds to wait between recalculating path to player
    int movementRecalculation;
    Vector3 playerDirection;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        time = Time.time;
        movementRecalculation = Random.Range(3, 6);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        time = Time.time;
        Move();
    }

    private void Move()
    {
        // Calculate the distance to the player
        playerDistance = Mathf.Abs(Vector3.Distance(playerTransform.Value.position, transform.position));

        // Recalculate trajectory when recalculation limit as lapsed, or when the target location has been reached (prevent walking in place)
        if ((time % movementRecalculation == 0) || Vector3.Distance(transform.position, playerDirection) < 1)
        {
            // Set a new trajectory towards the player, but add some randomness to the trajectory +-5 on X and Z axes
            playerDirection = new Vector3(playerTransform.Value.position.x + Random.Range(-5, 5), transform.position.y, playerTransform.Value.position.z + Random.Range(-5, 5));
        }
        if (playerDistance < alertness.Value && playerDistance >= 3 && canMove)
        {
            anim.SetFloat("MoveSpeed", 1);
            // The step size is equal to speed times frame time.
            float step = 5 * Time.deltaTime;

            // Move position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, playerDirection, step);

            // Rotate to face player
            transform.LookAt(playerDirection);
        }
        else
        {
            anim.SetFloat("MoveSpeed", 0);
            
            // When close to the player, aim precisely
            if (playerDistance < 3)
            {
                playerDirection = new Vector3(playerTransform.Value.position.x, transform.position.y, playerTransform.Value.position.z);
            }
            // Rotate to face player
            transform.LookAt(playerDirection);
        }

        // Only attack if the player is within striking distance
        if (playerDistance < 5)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Random.Range(0, 100) == 1)
        {
            anim.SetTrigger("Combo1");
        }
    }

    private void SetCanMove(int v)
    {
        if (v == 1)
        {
            canMove = true;
        }

        if (v == 0)
        {
            canMove = false;
        }
    }

    private void SetIsInvincible(int v)
    {
        if (v == 1)
        {
            isInvincible = true;
        }

        if (v == 0)
        {
            isInvincible = false;
        }

    }

    void TakeDamage()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            //            canMove = false;
            anim.SetTrigger("Knockback1");
            health -= 50;
        }

        if (health <= 0)
        {
            anim.SetTrigger("Knockback3");
            anim.SetBool("EnemyIsAlive", false);
        }
    }

    private void AnimDestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "AttackCollider" && other.transform.parent.name == "Player")
        {
            TakeDamage();
        }
    }
}
