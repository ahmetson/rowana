using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SetMusicSounds : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider volumeSlider;
    public Button defoultButton;
    public Button closeButton;
    public Toggle muteToggle;
    

    //Yellow setup menu
    public void SetVolumeYellow(float sliderValue)
    { 
        mixer.SetFloat("YellowVolume", sliderValue);
    }

    public void SetMuteYellow()
    {
        if (muteToggle.isOn)
        {
            mixer.SetFloat("YellowVolume", -80f);
            volumeSlider.enabled = false;
        } else {
            volumeSlider.enabled = true;
            mixer.SetFloat("YellowVolume", volumeSlider.value);
        }
    }

    public void SetDefoultYellow(bool buttonValue)
    {
        muteToggle.isOn = false;
        mixer.SetFloat("YellowVolume", 0);
        volumeSlider.value = 0f;
        volumeSlider.enabled = true;
    }

    public void SetCloseMenuYellow()
    {
        
    }



    public void SetVolumeGrean(float sliderValue)
    {
        mixer.SetFloat("GreanVolume", sliderValue);
    }
    public void SetVolumeOrange(float sliderValue)
    {
        mixer.SetFloat("OrangeVolume", sliderValue);
    }
    public void SetVolumeBlue(float sliderValue)
    {
        mixer.SetFloat("BlueVolume", sliderValue);
    }

}
