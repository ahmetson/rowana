using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
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
    public Toggle soloToggle;
    public Toggle testModeToggle;
    public Button changeSceneButton;

    private void Start()
    {
        mixer.SetFloat("ExampleVolume", -80f);
        mixer.SetFloat("MethronomVolume", -80f);
    }

    private void Update()
    {
        CheckToggle();
    }

    public void CheckToggle()
    {
        if (testModeToggle.isOn)
        {
            foreach (Button button in changeMusic.allButton)
            {
                button.interactable = true;
            }
        }
    }
    //Yellow setup menu

    public void SetVolumeYellow()
    { 
        mixer.SetFloat("YellowVolume", volumeSliderYellow.value);
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
        volumeSliderYellow.value = 0f;
        volumeSliderYellow.enabled = true;
        mixer.SetFloat("YellowVolume", 0);
        changeMusic.StopMusics();
        testModeToggle.isOn = false;
        foreach (Button button in changeMusic.allButton)
        {
            button.interactable = true;
        }
    }
    public void SetSetupYellow()
    {
        SceneManager.LoadScene("SetupYellow");
    }

    //Grean setup menu

    public void SetVolumeGrean()
    {
        mixer.SetFloat("GreanVolume", volumeSliderGrean.value);
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
        mixer.SetFloat("GreanVolume", 0);
        volumeSliderGrean.value = 0f;
        volumeSliderGrean.enabled = true;
        changeMusic.StopMusics();
        testModeToggle.isOn = false;
        foreach (Button button in changeMusic.allButton)
        {
            button.interactable = true;
        }
    }

    //Orange setup menu

    public void SetVolumeOrange()
    {
        mixer.SetFloat("OrangeVolume", volumeSliderOrange.value);
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
        mixer.SetFloat("OrangeVolume", 0); 
        volumeSliderOrange.value = 0f;
        volumeSliderOrange.enabled = true;
        changeMusic.StopMusics();
        testModeToggle.isOn = false;
        foreach (Button button in changeMusic.allButton)
        {
            button.interactable = true;
        }
    }

    //Blue setup menu
    public void SetVolumeBlue()
    {
        mixer.SetFloat("BlueVolume", volumeSliderBlue.value);
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
        volumeSliderBlue.value = 0f;
        volumeSliderBlue.enabled = true;
        mixer.SetFloat("BlueVolume", 0);
        changeMusic.StopMusics();
        testModeToggle.isOn = false;
        foreach (Button button in changeMusic.allButton)
        {
            button.interactable = true;
        }
    }
}
