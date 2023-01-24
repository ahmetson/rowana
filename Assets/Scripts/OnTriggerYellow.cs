using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerYellow : MonoBehaviour
{
    public SetMusicSounds set;

    private void OnTriggerEnter(Collider other)
    {
        set.mixer.SetFloat("GreanVolume", -80f);
        set.mixer.SetFloat("OrangeVolume", -80f);
        set.mixer.SetFloat("BlueVolume", -80f);
    }
    private void OnTriggerExit(Collider other)
    {
        set.mixer.SetFloat("GreanVolume", set.volumeSliderGrean.value);
        set.mixer.SetFloat("OrangeVolume", set.volumeSliderOrange.value);
        set.mixer.SetFloat("BlueVolume", set.volumeSliderBlue.value);
    }
}
