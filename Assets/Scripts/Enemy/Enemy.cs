﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Animator anim;
    private Rigidbody body;
    public GameObject attackCollider;
    public TransformVariable playerTransform;
    public float playerActivationDistance = 20;
    float playerDistance;
    public float moveSpeed = 5;
    private bool canMove = true;
    private bool isInvincible = false;

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
        Attack();
        if (playerDistance < playerActivationDistance && playerDistance >= 3 && canMove)
        {
            anim.SetFloat("MoveSpeed", 1);
            // The step size is equal to speed times frame time.
            float step = 5 * Time.deltaTime;

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.Value.position, step);

            // Rotate to face player
            transform.LookAt(playerTransform.Value.position);

        } else
        {
            anim.SetFloat("MoveSpeed", 0);
        }
    }

    private void Attack()
    {
        if (Random.Range(0, 50) == 5)
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "AttackCollider" && other.transform.parent.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}