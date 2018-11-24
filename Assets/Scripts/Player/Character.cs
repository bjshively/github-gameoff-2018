using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public bool canMove = true;
    public bool isInvincible = false;
    protected Animator anim;
    protected Rigidbody body;

    protected virtual void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
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

    protected void Die(string animBool)
    {
        anim.SetTrigger("Knockback3");
        anim.SetBool(animBool, false);
    }
}
