using UnityEngine;
using System;

public class Oscillator : MonoBehaviour
{
    public double frequency = 440;
    public double gain = 0; 
    private double increment;
    private double phase;
    private double sampling_frequency = 48000;
    public float volume = 0.1f;
    public float[] frequencies;
    public int thisFreq;


    private void Start()
    {
        gain = 0;
        frequencies = new float[8];
        frequencies[0] = 440;
        frequencies[1] = 494;
        frequencies[2] = 554;
        frequencies[3] = 587;
        frequencies[4] = 659;
        frequencies[5] = 740;
        frequencies[6] = 831;
        frequencies[7] = 880;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            gain = volume;
            frequency = frequencies[thisFreq];
            thisFreq += 1;
            thisFreq = thisFreq % frequencies.Length;
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            gain = 0;
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2 * Math.PI / sampling_frequency;
        for (var i = 0; i < data.Length; i = i + channels)
        {
            phase = phase + increment;

            data[i] = (float)(gain * Math.Sin(phase));
 
            if (channels == 2) data[1 + i] = data[i];
            if (phase > 2 * Math.PI) phase = 0;
        }
    }
}