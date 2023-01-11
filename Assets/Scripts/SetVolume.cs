using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetVolumeYellow(float sliderValue)
    {
        mixer.SetFloat("YellowVolume", Mathf.Log10(sliderValue) * 20); 
    }
    public void SetVolumeGrean(float sliderValue)
    {
        mixer.SetFloat("GreanVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void SetVolumeOrange(float sliderValue)
    {
        mixer.SetFloat("OrangeVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void SetVolumeBlue(float sliderValue)
    {
        mixer.SetFloat("BlueVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void SetVolumeExample(Toggle toggle)
    {
          
            
    }
}
