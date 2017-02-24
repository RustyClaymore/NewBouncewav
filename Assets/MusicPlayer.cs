/*
* Copyright (c) Yakin Najahi
* Twitter: https://twitter.com/nightkenny
* 
* Description:
*/

using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    #region Variables
    public AudioClip[] rewindClips;
    public AudioClip[] musicClips;

    public bool startedPlaying = false;

    private GameObject player;
    private AudioSource audioSource;
	#endregion
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (startedPlaying)
        {
            if (!audioSource.isPlaying)
            {
                PlayRandomClip();
            }
        }
    }

    public IEnumerator PlayRewindSound()
    {
        audioSource.clip = rewindClips[0];
        audioSource.Play();
        yield return new WaitForSeconds(rewindClips[0].length);
        StartCoroutine(PlayRewindLoop());
    }

    IEnumerator PlayRewindLoop()
    {
        audioSource.clip = rewindClips[1];
        audioSource.Play();
        yield return new WaitForSeconds(rewindClips[1].length);

        if (!player.GetComponent<BounceScript3D>().GetGameStart())
        {
            StartCoroutine(PlayRewindLoop());
        }
    }

    public void PlayRandomClip()
    {
        startedPlaying = true;
        int randVal = Random.Range(0, musicClips.Length);

        audioSource.clip = musicClips[randVal];
        audioSource.Play();
    }

    public void StopCurrentClip()
    {
        audioSource.Stop();
    }
}
