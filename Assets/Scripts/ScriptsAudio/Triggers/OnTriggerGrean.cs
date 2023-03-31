using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerGrean : MonoBehaviour
{
    public SetMusicSounds set;

    private void OnTriggerExit(Collider other)
    {
        set.mixer.SetFloat("YellowVolume", set.volumeSliderYellow.value);
        set.mixer.SetFloat("OrangeVolume", set.volumeSliderOrange.value);
        set.mixer.SetFloat("BlueVolume", set.volumeSliderBlue.value);
        set.soloToggle.isOn = false;
    }
}
