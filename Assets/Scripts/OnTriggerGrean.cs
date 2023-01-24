using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerGrean : MonoBehaviour
{
    public SetMusicSounds set;

    private void OnTriggerEnter(Collider other)
    {
        set.mixer.SetFloat("YellowVolume", -80f);
        set.mixer.SetFloat("OrangeVolume", -80f);
        set.mixer.SetFloat("BlueVolume", -80f);
    }
    private void OnTriggerExit(Collider other)
    {
        set.mixer.SetFloat("YellowVolume", set.volumeSliderYellow.value);
        set.mixer.SetFloat("OrangeVolume", set.volumeSliderOrange.value);
        set.mixer.SetFloat("BlueVolume", set.volumeSliderBlue.value);
    }
}
