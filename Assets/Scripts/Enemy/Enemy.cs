using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Animator anim;
    private Rigidbody body;
    public GameObject attackCollider;
    public TransformVariable playerTransform;
    public FloatVariable enemyHitTime;
    public float playerActivationDistance = 20;
    float playerDistance;
    public float moveSpeed = 5;
    private bool canMove = true;
    private bool isInvincible = false;
    private float health = 150;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        playerDistance = Mathf.Abs(Vector3.Distance(playerTransform.Value.position, transform.position));
        Move();
    }

    private void Move()
    {
        if (playerDistance < playerActivationDistance && playerDistance >= 3 && canMove)
        {
            anim.SetFloat("MoveSpeed", 1);
            // The step size is equal to speed times frame time.
            float step = 5 * Time.deltaTime;

            Vector3 playerDirection = new Vector3(playerTransform.Value.position.x, transform.position.y, playerTransform.Value.position.z);

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, playerDirection, step);

            // Rotate to face player
            transform.LookAt(playerDirection);

        } else
        {
            anim.SetFloat("MoveSpeed", 0);
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
            canMove = false;
            anim.SetTrigger("Knockback1");
            health -= 50;
        }
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "AttackCollider" && other.transform.parent.name == "Player")
        {
            enemyHitTime.Value = Time.time;
            TakeDamage();
            
        }
    }
}
