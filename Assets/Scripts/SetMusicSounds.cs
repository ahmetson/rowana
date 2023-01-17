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
    public ChangeMusic changeMusic;

    public Slider volumeSliderYellow;
    public Slider volumeSliderOrange;
    public Slider volumeSliderGrean;
    public Slider volumeSliderBlue;
    public Button defoultButton;
    public Toggle muteToggle;
    public Toggle soloToggle;

    private void Start()
    {
        mixer.SetFloat("ExampleVolume", -80f);
    }

    //Yellow setup menu

    public void SetVolumeYellow()
    { 
        mixer.SetFloat("YellowVolume", volumeSliderYellow.value);
    }
    public void SetMuteYellow()
    {
        if (muteToggle.isOn)
        {
            mixer.SetFloat("YellowVolume", -80f);
            volumeSliderYellow.enabled = false;
        } else {
            volumeSliderYellow.enabled = true;
            mixer.SetFloat("YellowVolume", volumeSliderYellow.value);
        }
    }

    public void SetSoloYellow()
    {
        if (soloToggle.isOn)
        {
            mixer.SetFloat("GreanVolume", -80f);
            mixer.SetFloat("OrangeVolume", -80f);
            mixer.SetFloat("BlueVolume", -80f);
            volumeSliderGrean.enabled = false;
            volumeSliderOrange.enabled = false;
            volumeSliderBlue.enabled = false;
        }
        else
        {
            mixer.SetFloat("GreanVolume", volumeSliderGrean.value);
            mixer.SetFloat("OrangeVolume", volumeSliderOrange.value);
            mixer.SetFloat("BlueVolume", volumeSliderBlue.value);
            volumeSliderGrean.enabled = true;
            volumeSliderOrange.enabled = true;
            volumeSliderBlue.enabled = true;
        }
    }

    public void SetDefoultYellow()
    {
        soloToggle.isOn = false;
        muteToggle.isOn = false;
        mixer.SetFloat("YellowVolume", 0);
        volumeSliderYellow.value = 0f;
        volumeSliderYellow.enabled = true;
        changeMusic.StopMusics();
    }

    //Grean setup menu

    public void SetVolumeGrean()
    {
        mixer.SetFloat("GreanVolume", volumeSliderGrean.value);
    }

    public void SetMuteGrean()
    {
        if (muteToggle.isOn)
        {
            mixer.SetFloat("GreanVolume", -80f);
            volumeSliderGrean.enabled = false;
        }
        else
        {
            volumeSliderGrean.enabled = true;
            mixer.SetFloat("GreanVolume", volumeSliderGrean.value);
        }
    }

    public void SetSoloGrean()
    {
        if (soloToggle.isOn)
        {
            mixer.SetFloat("YellowVolume", -80f);
            mixer.SetFloat("OrangeVolume", -80f);
            mixer.SetFloat("BlueVolume", -80f);
            volumeSliderYellow.enabled = false;
            volumeSliderOrange.enabled = false;
            volumeSliderBlue.enabled = false;
        }
        else
        {
            mixer.SetFloat("YellowVolume", volumeSliderYellow.value);
            mixer.SetFloat("OrangeVolume", volumeSliderOrange.value);
            mixer.SetFloat("BlueVolume", volumeSliderBlue.value);
            volumeSliderYellow.enabled = true;
            volumeSliderOrange.enabled = true;
            volumeSliderBlue.enabled = true;
        }
    }

    public void SetDefoultGrean()
    {
        soloToggle.isOn = false;
        muteToggle.isOn = false;
        mixer.SetFloat("GreanVolume", 0);
        volumeSliderGrean.value = 0f;
        volumeSliderGrean.enabled = true;
        changeMusic.StopMusics();
    }

    //Orange setup menu

    public void SetVolumeOrange()
    {
        mixer.SetFloat("OrangeVolume", volumeSliderOrange.value);
    }

    public void SetMuteOrange()
    {
        if (muteToggle.isOn)
        {
            mixer.SetFloat("OrangeVolume", -80f);
            volumeSliderOrange.enabled = false;
        }
        else
        {
            volumeSliderOrange.enabled = true;
            mixer.SetFloat("OrangeVolume", volumeSliderOrange.value);
        }
    }

    public void SetSoloOrange()
    {
        if (soloToggle.isOn)
        {
            mixer.SetFloat("YellowVolume", -80f);
            mixer.SetFloat("GreanVolume", -80f);
            mixer.SetFloat("BlueVolume", -80f);
            volumeSliderYellow.enabled = false;
            volumeSliderGrean.enabled = false;
            volumeSliderBlue.enabled = false;
        }
        else
        {
            mixer.SetFloat("YellowVolume", volumeSliderYellow.value);
            mixer.SetFloat("GreanVolume", volumeSliderGrean.value);
            mixer.SetFloat("BlueVolume", volumeSliderBlue.value);
            volumeSliderYellow.enabled = true;
            volumeSliderGrean.enabled = true;
            volumeSliderBlue.enabled = true;
        }
    }

    public void SetDefoultOrange()
    {
        soloToggle.isOn = false;
        muteToggle.isOn = false;
        mixer.SetFloat("OrangeVolume", 0);
        volumeSliderOrange.value = 0f;
        volumeSliderOrange.enabled = true;
        changeMusic.StopMusics();
    }

    //Blue setup menu
    public void SetVolumeBlue()
    {
        mixer.SetFloat("BlueVolume", volumeSliderBlue.value);
    }

    public void SetMuteBlue()
    {
        if (muteToggle.isOn)
        {
            mixer.SetFloat("BlueVolume", -80f);
            volumeSliderBlue.enabled = false;
        }
        else
        {
            volumeSliderBlue.enabled = true;
            mixer.SetFloat("BlueVolume", volumeSliderBlue.value);
        }
    }

    public void SetSoloBlue()
    {
        if (soloToggle.isOn)
        {
            mixer.SetFloat("GreanVolume", -80f);
            mixer.SetFloat("OrangeVolume", -80f);
            mixer.SetFloat("YellowVolume", -80f);
            volumeSliderGrean.enabled = false;
            volumeSliderOrange.enabled = false;
            volumeSliderYellow.enabled = false;
        }
        else
        {
            mixer.SetFloat("GreanVolume", volumeSliderGrean.value);
            mixer.SetFloat("OrangeVolume", volumeSliderOrange.value);
            mixer.SetFloat("YellowVolume", volumeSliderYellow.value);
            volumeSliderGrean.enabled = true;
            volumeSliderOrange.enabled = true;
            volumeSliderYellow.enabled = true;
        }
    }

    public void SetDefoultBlue()
    {
        soloToggle.isOn = false;
        muteToggle.isOn = false;
        mixer.SetFloat("BlueVolume", 0);
        volumeSliderBlue.value = 0f;
        volumeSliderBlue.enabled = true;
        changeMusic.StopMusics();
    }

}
