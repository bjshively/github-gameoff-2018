using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody body;
    private Animator anim;


    private float horizontal;
    private float vertical;
    private int targetRotation = 90;


    public IntVariable health;
    public IntVariable maxHealth;
    public FloatVariable moveSpeed;

    // Use this for initialization
    void Start () {
        health.Value = maxHealth.Value;
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Move(horizontal, vertical);
      
        if (Input.GetButtonDown("Fire1"))
        {
           

            Punch();
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
        //if (h > 0)
        //{
        //    h = moveSpeed.Value;
        //} else if (h < 0)
        //{
        //    h = moveSpeed.Value * -1f;
        //}

        //if (v > 0)
        //{
        //    v = moveSpeed.Value;
        //}
        //else if (h < 0)
        //{
        //    v = moveSpeed.Value * -1f;
        //}

        body.velocity = new Vector3(h, 0.0f, v).normalized;
        
        //body.velocity = new Vector2(moveSpeed * Mathf.Sign(h), body.velocity.y);
        //transform.position += moveNormal * Time.deltaTime * MoveRate.Value;
    }

    private void Punch()
    {
        Turn();
        anim.SetTrigger("Punch");
        Debug.Log("Punch");
    }

    private void Turn()
    {
        {
            StartCoroutine(Rotate());
        }
    }

    IEnumerator Rotate()
    {
        float moveSpeed = 10f;
        while (transform.rotation.y != targetRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRotation, 0), moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, targetRotation, 0);
        targetRotation *= -1;
        yield return null;
    }

    private void Die()
    {
        // Debug.Log("Dead");
    }
}
