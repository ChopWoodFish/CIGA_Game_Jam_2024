using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;


    public AudioClip stepSound; // �����Ծ��ص���Ч
    public AudioClip tunaSound; //��ͷ��Ч





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

    public void PlayStepSound()
    {
        audioSource.clip = stepSound;
        audioSource.Play();
    }

    public void PlaytunaSound()
    {
        audioSource.clip = tunaSound;
        audioSource.Play();
    }


}

