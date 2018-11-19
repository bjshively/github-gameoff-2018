using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody body;


    private float horizontal;
    private float vertical;
    public IntVariable health;
    public IntVariable maxHealth;

    // Use this for initialization
    void Start () {
        health.Value = maxHealth.Value;
        body = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Move(horizontal, vertical);
        Debug.Log(health.Value);


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
        body.velocity = new Vector3(h, 0.0f, v).normalized;
        
        //body.velocity = new Vector2(moveSpeed * Mathf.Sign(h), body.velocity.y);
        //transform.position += moveNormal * Time.deltaTime * MoveRate.Value;
    }

    private void Die()
    {
        Debug.Log("Dead");
    }
}
