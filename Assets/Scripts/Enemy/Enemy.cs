using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public GameObject attackCollider;
    public TransformVariable playerTransform;
    public FloatReference enemyHitTime;
    public float playerDistance;
    public bool isAwake = true;

    // when does enemy notice the player
    public FloatReference playerInSight;
    // when should enemy start to attack
    public float playerInRange = 3;

    public FloatReference MoveSpeed;
    public int health;
    protected float time;

    //Audio Events
    public AK.Wwise.Event EnemyDamagedSound;
    public AK.Wwise.Event EnemySwingSound;


    // Pick a random number of seconds to wait between recalculating path to player
    public int movementRecalculation;
    protected Vector3 playerDirection;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        time = Time.time;
        movementRecalculation = Random.Range(3, 6);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isAwake)
        {
            time = Time.time;
            Move();
            anim.SetFloat("DistanceToPlayer", playerDistance);
        } else
        {
            anim.SetFloat("MoveSpeed", 0);
        }
    }

    void PlayFallDownSound()
    {
        EnemyDamagedSound.Post(gameObject);
    }

    protected virtual void Move()
    {
        // Calculate the distance to the player
        playerDistance = Mathf.Abs(Vector3.Distance(playerTransform.Value.position, transform.position));

        // Recalculate trajectory when recalculation limit as lapsed, or when the target location has been reached (prevent walking in place)
        if ((time % movementRecalculation == 0) || Vector3.Distance(transform.position, playerDirection) < 1)
        {
            // Set a new trajectory towards the player, but add some randomness to the trajectory +-5 on X and Z axes
            playerDirection = new Vector3(playerTransform.Value.position.x + Random.Range(-5, 5), transform.position.y, playerTransform.Value.position.z + Random.Range(-5, 5));
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
            if (Random.Range(0, 100) == 1)
            {
                Attack();
                
            }
        }
    }

    protected virtual void Attack()
    {
        Melee();
        //if (canMove)
        //{
        //    anim.SetTrigger("Combo1");
            EnemySwingSound.Post(gameObject);
        //}
    }

    void TakeDamage()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            //            canMove = false;
            health -= 1;
            if (health <= 0)
            {
                Die("EnemyIsDead");
                EnemyDamagedSound.Post(gameObject);
            }
            else
            {
                anim.SetTrigger("Knockback1");
                EnemyDamagedSound.Post(gameObject);
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
        anim.SetBool("Awake", state);
    }

}
