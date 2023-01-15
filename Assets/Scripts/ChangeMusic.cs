using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusic : MonoBehaviour
{
    public AudioSource audioSource;


    public void ChangeMusics(AudioClip music)
    {
        audioSource.clip = music;
    }
}
