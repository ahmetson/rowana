using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System.IO;

public class Recorders : MonoBehaviour
{
    public AudioRenderer rec;
    private string path;
    private int numberFile = 1;

    public void Start()
    {
        path = Application.streamingAssetsPath + string.Format(@"\Recorded\YellowSetup.wav", numberFile);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rec.Rendering = true;
        } else if (Input.GetKeyDown(KeyCode.T))
        {
            numberFile += 1;
            rec.Save(path);
            rec.Rendering = false;
        }
    }

}