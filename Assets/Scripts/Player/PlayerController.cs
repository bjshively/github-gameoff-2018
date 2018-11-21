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

    public FloatVariable health;
    public FloatVariable maxHealth;
    public FloatVariable moveSpeed;
    public TransformVariable playerTransform;


    // Use this for initialization
    void Start () {
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
            anim.SetTrigger("Punch");
        }

        if (health.Value <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        health.ApplyChange(-10);
    }

    private void Move(float h, float v)
    {
        // If you can't move or player isn't pushing wasd, set velocity to 0
        if (!canMove || (h == 0 & v == 0))
        {
            body.velocity = Vector3.zero;
            anim.SetFloat("MoveSpeed", 0);
        }

        // Otherwise, move 
        else
        {
            body.velocity = new Vector3(h * moveSpeed.Value, 0.0f, v * moveSpeed.Value);
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
        // Reload current scene on death
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "AttackCollider")
        {
            health.ApplyChange(-10);
        }
    }
}
