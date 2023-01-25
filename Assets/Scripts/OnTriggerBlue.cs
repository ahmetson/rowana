using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerBlue : MonoBehaviour
{
    public SetMusicSounds set;

    private void OnTriggerExit(Collider other)
    {
        set.mixer.SetFloat("GreanVolume", set.volumeSliderGrean.value);
        set.mixer.SetFloat("OrangeVolume", set.volumeSliderOrange.value);
        set.mixer.SetFloat("YellowVolume", set.volumeSliderYellow.value);
        set.soloToggle.isOn = false;
    }
}
