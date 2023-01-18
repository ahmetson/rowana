using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioMixer mixer;

    public void ChangeMusics(AudioClip music)
    {
        audioSource.clip = music;
        mixer.SetFloat("MethronomVolume", -15f);
        Invoke("TurnOff", 4f);
    }

    public void TurnOff()
    {
        mixer.SetFloat("MethronomVolume", -80f);
    }

    public void StopMusics()
    {
        audioSource.clip = null;
    }
}
