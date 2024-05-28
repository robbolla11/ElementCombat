using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip background;
    public AudioClip hover;
    public AudioClip gong;

    public AudioClip click;

    public AudioClip tick;

    public AudioClip click2;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void playSFX(AudioClip clip, float volume = 1.0f)
    {
        sfxSource.volume = volume;
        sfxSource.PlayOneShot(clip);
    }
}
