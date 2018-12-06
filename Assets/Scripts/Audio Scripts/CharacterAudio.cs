using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour {

    public WwiseEventVariable PlayerDamageSound;
    public WwiseEventVariable PlayerDeathSound;
    public WwiseEventVariable PlayerSwingSound;
    public WwiseEventVariable FootStepSound;
    public WwiseEventVariable EnemyDamagedSound;
    public WwiseEventVariable EnemySwingSound;
    public WwiseEventVariable EnemyDeathSound;
    public WwiseEventVariable BulletSound;

    public void PlayFootstepSound()
    {
        FootStepSound.Value.Post(gameObject);
    }

    public void PlaySwingSound()
    {
        PlayerSwingSound.Value.Post(gameObject);
    }

    public void PlayDeathSound()
    {
        EnemyDeathSound.Value.Post(gameObject);
    }

    public void PlayFallDownSound()
    {
        PlayerDamageSound.Value.Post(gameObject);
    }

    public void PlayPlayerDamagedSound()
    {
        PlayerDamageSound.Value.Post(gameObject);
    }

    public void PlayPlayerDeathSound()
    {
        AkSoundEngine.StopAll();
        PlayerDeathSound.Value.Post(gameObject);
    }

    private void PlayBulletSound()
    {
        BulletSound.Value.Post(gameObject);
    }
}
