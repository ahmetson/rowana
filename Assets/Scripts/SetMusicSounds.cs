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
    public TriggerBox triggerBox;

    public Slider volumeSliderYellow;
    public Slider volumeSliderOrange;
    public Slider volumeSliderGrean;
    public Slider volumeSliderBlue;
    public Button defoultButton;

    private void Start()
    {
        mixer.SetFloat("ExampleVolume", -80f);
        mixer.SetFloat("MethronomVolume", -80f);
    }

    //Yellow setup menu

    public void SetVolumeYellow()
    { 
        mixer.SetFloat("YellowVolume", volumeSliderYellow.value);
    }

    public void SetDefoultYellow()
    {
        volumeSliderYellow.value = 0f;
        volumeSliderYellow.enabled = true;
        mixer.SetFloat("YellowVolume", 0);

        changeMusic.StopMusics();
    }

    //Grean setup menu

    public void SetVolumeGrean()
    {
        mixer.SetFloat("GreanVolume", volumeSliderGrean.value);
    }

    public void SetDefoultGrean()
    {
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

    public void SetDefoultOrange()
    {
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

    public void SetDefoultBlue()
    {
        volumeSliderBlue.value = 0f;
        volumeSliderBlue.enabled = true;
        mixer.SetFloat("BlueVolume", 0);
        
        changeMusic.StopMusics();
    }
}
