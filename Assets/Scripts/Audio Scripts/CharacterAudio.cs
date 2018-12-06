using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour {

    public WwiseEventVariable DamagedSFX;
    public WwiseEventVariable DeathSFX;
    public WwiseEventVariable FallDownSFX;
    public WwiseEventVariable FootstepSFX;
    public WwiseEventVariable GunshotSFX;
    public WwiseEventVariable MeleeSFX;
    public WwiseEventVariable PlayerDeathSFX;

    public void PlayAudioFootstep()
    {
        FootstepSFX.Value.Post(gameObject);
    }

    public void PlayAudioMelee()
    {
        MeleeSFX.Value.Post(gameObject);
    }

    public void PlayAudioFallDown()
    {
        FallDownSFX.Value.Post(gameObject);
    }

    public void PlayAudioDamaged()
    {
        DamagedSFX.Value.Post(gameObject);
    }

    public void PlayAudioDeath()
    {
        DeathSFX.Value.Post(gameObject);
    }

    // Player gets special death audio
    public void PlayAudioPlayerDeath()
    {
        AkSoundEngine.StopAll();
        PlayerDeathSFX.Value.Post(gameObject);
    }

    private void PlayAudioGunshot()
    {
        GunshotSFX.Value.Post(gameObject);
    }
}
