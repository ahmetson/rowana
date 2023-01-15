using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusicYellow : MonoBehaviour
{
    public AudioSource audioSource;


    public void ChangeMusic(AudioClip music)
    {
        audioSource.clip = music;
    }
}
