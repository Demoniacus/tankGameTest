using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource vfxAudioSource;
    public AudioClip breaksSound;

    public AudioClip movingSound;

    public AudioClip fireSound;

    public AudioClip hitTargetSound;

    public AudioClip hoverSound;
    
    public void PlayMovingSound() {
        //If the running sound is not already playing, play it on loop
        if(!vfxAudioSource.isPlaying){
            vfxAudioSource.volume = 0.9f;            
            vfxAudioSource.pitch = 0.4f;
            vfxAudioSource.clip = movingSound;
            vfxAudioSource.loop = true;
            vfxAudioSource.Play();
        }
    }

    public void PlayBreakingSound() {
        vfxAudioSource.volume = 0.1f;
        vfxAudioSource.loop = false;
        vfxAudioSource.pitch = 0.6f;
        vfxAudioSource.clip = breaksSound;
        vfxAudioSource.Play();
    }

    public void PlayHitTargetSound() {
        vfxAudioSource.volume = 1f;
        vfxAudioSource.pitch = 1f;        
        vfxAudioSource.clip = hitTargetSound;
        vfxAudioSource.Play();
    }


    public void PlayFireSound() {
        vfxAudioSource.volume = 1f;
        vfxAudioSource.pitch = 1f;
        vfxAudioSource.clip = fireSound;
        vfxAudioSource.Play();
    }

    public void PlayButtonHoverSound() {
        vfxAudioSource.volume = 1f;
        vfxAudioSource.pitch = 1f;
        vfxAudioSource.clip = hoverSound;
        vfxAudioSource.Play();
    }

}
