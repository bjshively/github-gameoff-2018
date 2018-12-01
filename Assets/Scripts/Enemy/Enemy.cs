using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public GameObject attackCollider;
    public TransformVariable playerTransform;
    public IntVariable playerHealth;
    float playerDistance;
    public bool isAwake = true;
    string[] attacks = { "Melee", "Combo2", "Combo3" };

    // when does enemy notice the player
    public FloatReference playerInSight;
    // when should enemy start to attack
    public float attackRange = 3;

    public FloatReference MoveSpeed;
    public int health;

    //Audio Events
    public AK.Wwise.Event EnemyDamagedSound;
    public AK.Wwise.Event EnemySwingSound;


    // Pick a random number of seconds to wait between recalculating path to player
    float movementRecalculation;
    protected Vector3 playerDirection;
    float aimTimer = 1;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        movementRecalculation = Random.Range(3, 6);
    }
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
        if (playerHealth.Value > 0)
        {
            if (isAwake)
            {
                Move();
                anim.SetFloat("DistanceToPlayer", playerDistance);
            }
            else
            {
                anim.SetFloat("MoveSpeed", 0);
            }
        }
    }

    void PlayFallDownSound()
    {
        EnemyDamagedSound.Post(gameObject);
    }

    protected virtual void Move()
    {
        if (canMove)
        {
            movementRecalculation -= Time.deltaTime;
            aimTimer -= Time.deltaTime;

            // Calculate the distance to the player
            playerDistance = Mathf.Abs(Vector3.Distance(playerTransform.Value.position, transform.position));

            // Recalculate trajectory when recalculation limit as lapsed, or when the target location has been reached (prevent walking in place)
            if (movementRecalculation <= 0 || Vector3.Distance(transform.position, playerDirection) < 1)
            {
                // Set a new trajectory towards the player, but add some randomness to the trajectory
                playerDirection = new Vector3(Mathf.Clamp(playerTransform.Value.position.x + Random.Range(-10, 10), 0, Mathf.Infinity), transform.position.y, Mathf.Clamp(playerTransform.Value.position.z + Random.Range(-10, 10), -20, 4));
                movementRecalculation = Random.Range(2, 4);
            }

            // If player in sight, but not in attack range, move towards player
            if (playerDistance < playerInSight.Value && playerDistance >= attackRange)
            {
                anim.SetFloat("MoveSpeed", MoveSpeed.Value);
                // The step size is equal to speed times frame time.
                float step = MoveSpeed.Value * Time.deltaTime;

                // Move position a step closer to the target.
                transform.LookAt(playerDirection);
                transform.position = Vector3.MoveTowards(transform.position, playerDirection, step);
            }
            else
            {
                anim.SetFloat("MoveSpeed", 0);

                // When close to the player, aim more precisely
                if (playerDistance < attackRange)
                {
                    if (aimTimer <= 0)
                    {
                        playerDirection = new Vector3(Mathf.Clamp(playerTransform.Value.position.x + Random.Range(-2, 2), 0, Mathf.Infinity), transform.position.y, Mathf.Clamp(playerTransform.Value.position.z + Random.Range(-2, 2), -20, 4));
                        aimTimer = 2;
                    }
                }
                // Rotate to face player
                transform.LookAt(playerDirection);
            }

            // Only attack if the player is within attack range
            if (playerDistance < attackRange)
            {
                if (Random.Range(0, 100) == 1)
                {
                    transform.LookAt(playerDirection);
                    Attack();
                }
            }
        }
    }

    protected virtual void Attack()
    {
        Melee();
        EnemySwingSound.Post(gameObject);
    }

    protected override void Melee()
    {
        if (canMove)
        {
            anim.SetTrigger(attacks[Random.Range(0, 3)]);
        }
    }

    void TakeDamage()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            health -= 1;
            EnemyDamagedSound.Post(gameObject);
            if (health <= 0)
            {
                Die("EnemyIsDead");
            }
            else
            {
                anim.SetTrigger("Knockback1");
            }
        }
    }

    private void AnimDestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.name == "AttackCollider" || other.name ==  "SmashCollider") && other.transform.parent.name == "Player")
        {
            TakeDamage();
        }
    }

    // Either wakeup (true) or put to sleep (false) enemy
    public void SetAwake(bool state)
    {
        isAwake = state;
        //anim.SetBool("Awake", state);
    }
}