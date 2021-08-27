using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] Music;
    private AudioSource soundSource;

    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        StartCoroutine(Playlist(Music));
    }

    IEnumerator Playlist(AudioClip[] clips)
    {
        soundSource.clip = clips[0];
        soundSource.Play();
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (!soundSource.isPlaying)
            {
                soundSource.clip = clips[1];
                soundSource.Play();
                soundSource.loop = true;
            }
        }
    }
}
