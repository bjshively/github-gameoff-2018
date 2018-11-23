using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private Rigidbody body;
    private Animator anim;
    public GameObject attackCollider;

    private float horizontal;
    private float vertical;
    private int targetRotation = -90;
    private int facing = 1;
    bool canMove = true;
    bool isInvincible = false;
    int currentCombo = 0;
    int currentKnockback = 0;

    public FloatVariable health;
    public FloatVariable maxHealth;
    public FloatVariable moveSpeed;
    public FloatVariable enemyHitTime;
    public FloatVariable playerHitTime;
    public TransformVariable playerTransform;

    string[] attacks = { "Combo1", "Combo2", "Combo3" };
    string[] knockbacks = { "Knockback1", "Knockback2", "Knockback3" };

    // Use this for initialization
    void Start () {
        enemyHitTime.Value = -10;
        health.Value = maxHealth.Value;
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // set the point for enemies to follow
        playerTransform.Value.position = transform.position;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Move(horizontal, vertical);

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (health.Value <= 0)
        {
            Die();
        }
    }

    private void Attack()
    {
        if (canMove)
        {
            // Increase the combo counter if the last hit was recent enough
            if ((Time.time - enemyHitTime.Value < 1) && currentCombo < 2)
            {
                currentCombo++;
            }
            else
            {
                currentCombo = 0; // Reset combo counter
            }

            // Execute the current combo move
            anim.SetTrigger(attacks[currentCombo]);
        }
    }

    private void Move(float h, float v)
    {
        // If you can't move or player isn't pushing wasd, set velocity to 0
        if (!canMove || (h == 0 & v == 0))
        {
            body.velocity = new Vector3(0, body.velocity.y, 0);
            anim.SetFloat("MoveSpeed", 0);
        }

        // Otherwise, move 
        else
        {
            body.velocity = new Vector3(h * moveSpeed.Value, body.velocity.y, v * moveSpeed.Value);
            anim.SetFloat("MoveSpeed", 1);

            Vector3 movement = new Vector3(h, 0.0f, v);
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * moveSpeed.Value * Time.deltaTime, Space.World);
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

    private void Die()
    {
        anim.SetTrigger("Knockback3");
        anim.SetBool("IsAlive", false);
    }

    private void ReloadScene()
    {
        // Reload current scene on death
        // to be called from animation
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void TakeDamage()
    {
       
        if (!isInvincible)
        {
            // Reset combo counter
            currentCombo = 0;
            isInvincible = true;

            if ((Time.time - playerHitTime.Value < 5) && currentKnockback < 2)
            {
                currentKnockback++;
            }
            else
            {
                currentKnockback = 0; // Reset player hit counter
            }
   

            playerHitTime.Value = Time.time;
            // Trigger the knockback
            anim.SetTrigger(knockbacks[currentKnockback]);
            health.ApplyChange(-10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "AttackCollider")
        {
            TakeDamage();
        }
    }
}
