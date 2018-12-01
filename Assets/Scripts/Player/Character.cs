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
    public AK.Wwise.Event EnemyDeathSound;

    public float DelayTime;
 
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

    //DeathSound Delay
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        EnemyDeathSound.Post(gameObject);
    }

    protected void Die(string s)
    {
        anim.SetTrigger(s);
        StartCoroutine(ExecuteAfterTime(DelayTime));
        
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
