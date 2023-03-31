using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSourceChange;
    public AudioMixer mixer;
    public List<Button> allButton;
    public SetMusicSounds set;
    public void ChangeMusics(AudioClip music)
    {
        if (!set.testModeToggle.isOn)
        {
            audioSourceChange.clip = null;
            audioSource.clip = music;
            mixer.SetFloat("MethronomVolume", -15f);
            Invoke("TurnOff", 8f);

            mixer.SetFloat("GreanVolume", set.volumeSliderGrean.value);
            mixer.SetFloat("OrangeVolume", set.volumeSliderOrange.value);
            mixer.SetFloat("BlueVolume", set.volumeSliderBlue.value);
            mixer.SetFloat("YellowVolume", set.volumeSliderYellow.value);
            mixer.SetFloat("MethronomVolume", -15f);

            Time.timeScale = 1;
        }
        else
        {
            audioSource.clip = null;
            audioSourceChange.Stop();
            audioSourceChange.clip = music;
            audioSourceChange.Play();

            mixer.SetFloat("GreanVolume", -80f);
            mixer.SetFloat("OrangeVolume", -80f);
            mixer.SetFloat("BlueVolume", -80f);
            mixer.SetFloat("YellowVolume", -80f);
            mixer.SetFloat("MethronomVolume", -80f);

            Time.timeScale = 0;
            
        }

        foreach (Button button in allButton)
        {
            button.interactable = true;
        }

    }

    public void TurnOff()
    {
        mixer.SetFloat("MethronomVolume", -80f);
    }

    public void StopMusics()
    {
        audioSource.clip = null;
        audioSourceChange.clip = null;
        Invoke("TurnOff", 0f);
    }
}
