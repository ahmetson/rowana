using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCompaire : MonoBehaviour
{
    public AudioListener listener;
    public AudioSource source1;
    public AudioSource source2;

    private void Awake()
    {
        listener = GetComponent<AudioListener>();
    }

    private void Update()
    {
        float[] samples1 = new float[source1.clip.samples * source1.clip.channels];
        float[] samples2 = new float[source2.clip.samples * source2.clip.channels];
        Debug.Log(source1.clip.GetData(samples1, 0));
        
    }
}
