using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class TriggerCrystal : MonoBehaviour
{
    public AudioMixer mixer;
    public SetMusicSounds setMusic;

    public AudioSource audioSourceY;
    public AudioSource audioSourceG;
    public AudioSource audioSourceO;
    public AudioSource audioSourceB;

    public AudioClip audioClipY;
    public AudioClip audioClipG;
    public AudioClip audioClipO;
    public AudioClip audioClipB;

    public Toggle yellowSoloToggle;
    public Toggle blueSoloToggle;
    public Toggle greanSoloToggle;
    public Toggle orangeSoloToggle;

    public GameObject panelVictory;


    private void OnTriggerEnter(Collider other)
    {
        mixer.SetFloat("YellowVolume", -80f);
        mixer.SetFloat("GreanVolume", -80f);
        mixer.SetFloat("OrangeVolume", -80f);
        mixer.SetFloat("BlueVolume", -80f);
        mixer.SetFloat("ExampleVolume", 0f);

        if (audioSourceY.clip == audioClipY && audioSourceG.clip == audioClipG && audioSourceO.clip == audioClipO && audioSourceB.clip == audioClipB)
        {
            panelVictory.SetActive(true);
}
    }
    private void OnTriggerExit(Collider other)
    {
        mixer.SetFloat("ExampleVolume", -80f);

        mixer.SetFloat("YellowVolume", setMusic.volumeSliderYellow.value);
        mixer.SetFloat("GreanVolume", setMusic.volumeSliderGrean.value);
        mixer.SetFloat("OrangeVolume", setMusic.volumeSliderOrange.value);
        mixer.SetFloat("BlueVolume", setMusic.volumeSliderBlue.value);

        /* 
         if (yellowSoloToggle.isOn)
         {
             mixer.SetFloat("YellowVolume", setMusic.volumeSliderYellow.value);
         } else 
         {
             mixer.SetFloat("GreanVolume", -80f);
             mixer.SetFloat("OrangeVolume", -80f);
             mixer.SetFloat("BlueVolume", -80f);
         }

         if (greanSoloToggle.isOn)
         {
             mixer.SetFloat("GreanVolume", setMusic.volumeSliderGrean.value);
         }
         else
         {
             mixer.SetFloat("YellowVolume", -80f);
             mixer.SetFloat("OrangeVolume", -80f);
             mixer.SetFloat("BlueVolume", -80f);  
         }

         if (orangeSoloToggle.isOn)
         {
             mixer.SetFloat("OrangeVolume", setMusic.volumeSliderOrange.value);
         }
         else
         {
             mixer.SetFloat("YellowVolume", -80f);
             mixer.SetFloat("BlueVolume", -80f);
             mixer.SetFloat("GreanVolume", -80f);
         }

         if (blueSoloToggle.isOn)
         {
             mixer.SetFloat("BlueVolume", setMusic.volumeSliderBlue.value);
         }
         else
         {
             mixer.SetFloat("YellowVolume", -80f);
             mixer.SetFloat("GreanVolume", -80f);
             mixer.SetFloat("OrangeVolume", -80f);
         }
         */
    }
}
