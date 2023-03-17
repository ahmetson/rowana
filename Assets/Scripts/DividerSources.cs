using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DividerSources : MonoBehaviour
{
    public void CheckPlatform(Collider platform)
    {
        
        AudioMixer mixer = platform.transform.GetComponentInChildren<AudioSource>().outputAudioMixerGroup.audioMixer;
        Debug.Log(mixer);

        if (platform.name == "HoldAreaA")
        {
            mixer.SetFloat("VolumeA", 0f);
            mixer.SetFloat("VolumeB", -80f);
            mixer.SetFloat("VolumeC", -80f);
        }        
        if (platform.name == "HoldAreaB")
        {
            mixer.SetFloat("VolumeA", -80f);
            mixer.SetFloat("VolumeB", 0f);
            mixer.SetFloat("VolumeC", -80f);
        }        
        if (platform.name == "HoldAreaC")
        {
            mixer.SetFloat("VolumeA", -80f);
            mixer.SetFloat("VolumeB", -80f);
            mixer.SetFloat("VolumeC", 0f);
        }
    }

    public void UnMute(Collider pickUp)
    {
        AudioMixer mixer = pickUp.transform.GetComponentInChildren<AudioSource>().outputAudioMixerGroup.audioMixer;

        mixer.SetFloat("VolumeA", 0f);
        mixer.SetFloat("VolumeB", 0f);
        mixer.SetFloat("VolumeC", 0f);
    }

    public void Mute(Collider droped)
    {
        AudioMixer mixer = droped.transform.GetComponentInChildren<AudioSource>().outputAudioMixerGroup.audioMixer;

        mixer.SetFloat("VolumeA", -80f);
        mixer.SetFloat("VolumeB", -80f);
        mixer.SetFloat("VolumeC", -80f);
    }
}
