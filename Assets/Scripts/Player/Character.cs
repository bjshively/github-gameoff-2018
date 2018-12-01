using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public bool canMove = true;
    public bool isInvincible = false;
    protected Animator anim;
    protected Rigidbody body;
    protected bool isAlive;

    // Audio variables
    public AK.Wwise.Event PlayerSwingSound;
    public AK.Wwise.Event FootStepSound;



    protected virtual void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isAlive = true;
    }

    protected virtual void Melee()
    {
                if (canMove)
        {
            anim.SetTrigger("Melee");
        }
    }

    protected void SetCanMove(int v)
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

    protected void SetIsInvincible(int v)
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
        FootStepSound.Post(gameObject);
        //AkSoundEngine.PostEvent("SFX_PlayerFootsteps", gameObject);    
    }

    private void PlaySwingSound()
    {
        PlayerSwingSound.Post(gameObject);
    }

}
