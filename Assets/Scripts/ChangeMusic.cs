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
    public List<Button> allButton;

    public void ChangeMusics(AudioClip music)
    {
        audioSource.clip = music;
        mixer.SetFloat("MethronomVolume", -15f);
        Invoke("TurnOff", 8f);
        foreach (Button button in allButton)
        {
            button.interactable = true;
        }
    }

    public void StartMusic()
    {

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
