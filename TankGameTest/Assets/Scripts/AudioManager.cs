using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip breaksSound;

    public AudioClip movingSound;

    public AudioClip fireSound;

    public AudioClip hitTargetSound;
    
    public void playMovingSound() {
        //If the running sound is not already playing, play it on loop
        if(!audioSource.isPlaying){
            audioSource.volume = 0.9f;            
            audioSource.pitch = 0.4f;
            audioSource.clip = movingSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void playBreakingSound() {
        audioSource.volume = 0.1f;
        audioSource.loop = false;
        audioSource.pitch = 0.6f;
        audioSource.clip = breaksSound;
        audioSource.Play();
    }

    public void playHitTargetSound() {
        audioSource.volume = 1f;
        audioSource.pitch = 1f;        
        audioSource.clip = hitTargetSound;
        audioSource.Play();
    }


    public void playFireSound() {
        audioSource.volume = 1f;
        audioSource.pitch = 1f;
        audioSource.clip = fireSound;
        audioSource.Play();
    }

    


}
