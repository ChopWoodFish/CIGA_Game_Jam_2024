using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmManager : MonoBehaviour
{
    public static bgmManager instance;
    public AudioSource audioSource;

    public AudioClip startBgm;
    public AudioClip endBgm;
    public AudioClip inBgm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

 /*   void Start()
    {
        PlaystartBgm();
    }
 */
    public void PlaystartBgm()
    {
        audioSource.clip = startBgm;
        audioSource.Play();

    }

    public void PlayendBgm()
    {
        audioSource.clip = endBgm;
        audioSource.Play();
    }

    public void PlayinBgm()
    {
        audioSource.clip = inBgm;
        audioSource.Play();
    }
}
