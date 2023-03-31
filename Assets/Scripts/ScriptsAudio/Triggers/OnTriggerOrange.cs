using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerOrange : MonoBehaviour
{
    public SetMusicSounds set;

    private void OnTriggerExit(Collider other)
    {
        set.mixer.SetFloat("GreanVolume", set.volumeSliderGrean.value);
        set.mixer.SetFloat("YellowVolume", set.volumeSliderYellow.value);
        set.mixer.SetFloat("BlueVolume", set.volumeSliderBlue.value);
        set.soloToggle.isOn = false;
    }
}
