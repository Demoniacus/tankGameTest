using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    AudioSource VFXAudioPlayer;

    [SerializeField]
    AudioClip hoverSound;


    public static AudioManager instance;

    private void Awake() {

        if (instance != null) 
            Destroy(gameObject);
        else 
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlayVFX(AudioClip clipToPlay, float volume, float pitch, bool loop) {
        VFXAudioPlayer.clip = clipToPlay;
        VFXAudioPlayer.volume = volume;
        VFXAudioPlayer.pitch = pitch;
        VFXAudioPlayer.loop = loop;
        VFXAudioPlayer.Play();
    }

    public void PlayHoverSound() {
        VFXAudioPlayer.clip = hoverSound;
        VFXAudioPlayer.Play();
    }

}
