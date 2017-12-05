using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource mainPlayer;
    public AudioClip mainSong;
    public AudioClip whistleSFX;
    public AudioClip clickSFX;
    public AudioClip releaseSFX;

	// Use this for initialization
	void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void PlayWhistle()
    {
        mainPlayer.PlayOneShot(whistleSFX);
    }

    public void PlayClick()
    {
        mainPlayer.PlayOneShot(clickSFX);
    }

    public void PlayRelease()
    {
        mainPlayer.PlayOneShot(releaseSFX);
    }
}
