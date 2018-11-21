using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody body;
    private Animator anim;
    public GameObject attackCollider;

    private float horizontal;
    private float vertical;
    private int targetRotation = -90;
    private int facing = 1;

    public IntVariable health;
    public IntVariable maxHealth;
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
        //body.velocity = new Vector3(h * moveSpeed.Value, 0.0f, v * moveSpeed.Value).normalized;
        
        if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
        {
            anim.SetFloat("MoveSpeed", 1);

            //Vector3 movement = new Vector3(h, 0.0f, v);
            //transform.rotation = Quaternion.LookRotation(movement);
            //transform.Translate(movement * moveSpeed.Value * Time.deltaTime, Space.World);
        }
        else
        {
            anim.SetFloat("MoveSpeed", 0);
        }
    }

    private void Die()
    {
        // Debug.Log("Dead");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "AttackCollider")
        {
            health.ApplyChange(-10);
            Debug.Log(health.Value);
        }
    }
}
