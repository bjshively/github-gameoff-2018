﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public bool canMove = true;
    public bool isInvincible = false;
    protected Animator anim;
    protected Rigidbody body;
    protected bool isAlive;

    // Audio variables
    public AK.Wwise.Event FootStepSound;
    public AK.Wwise.Switch GrassSwitch;
    public AK.Wwise.Switch DirtSwitch;
    public AK.Wwise.Switch ConcreteSwitch;


    protected virtual void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isAlive = true;
    }

    protected void Melee()
    {
        //        if (canMove)
        {
            anim.SetTrigger("Melee");
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

    protected void Die(string s)
    {
        anim.SetTrigger(s);
    }


    // AUDIO =================================
    // =======================================
    private void PlayFootstepSound()
    {
        //FootStepSound.Post(gameObject);
        //AK.SoundEngine.PostEvent("SFX_PlayerFootsteps", gameObject);
    }

}
