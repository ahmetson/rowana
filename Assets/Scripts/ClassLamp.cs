using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassLamp : MonoBehaviour 
{
    [Header("Level—onditions")]
    private GameObject platformA;
    private GameObject platformB;
    private GameObject platformC;

    private VictoryScript victoryScript;

    private void Awake()
    {
        platformA = GameObject.FindWithTag("musicA");
        platformB = GameObject.FindWithTag("musicB");
        platformC = GameObject.FindWithTag("musicC");   
        
        victoryScript = FindObjectOfType<VictoryScript>();
    }

    public void CheckTutorVictory()
    {
        int successLamps = 0;

        if (platformA.transform.childCount != 0) if (platformA.GetComponentInChildren<AudioSource>().clip.name.Contains(".melody"))  
        {
            successLamps += 1; 
        } else successLamps -= 1;

        if (platformB.transform.childCount != 0) if (platformB.GetComponentInChildren<AudioSource>().clip.name.Contains(".garmony"))
        {
            successLamps += 1;
        } else successLamps -= 1;

        if (platformC.transform.childCount != 0) if (platformC.GetComponentInChildren<AudioSource>().clip.name.Contains(".bass"))
        {
            successLamps += 1;
        } else successLamps -= 1;

        victoryScript.TutorVictory(successLamps);
    }

    public void CheckLevelOneVictory()
    {
        int successLamps = 0;

        if (platformA.transform.childCount != 0)
        {
            String name = platformA.GetComponentInChildren<AudioSource>().clip.name;
            if (name.Contains(".X")) { successLamps -= 1; }
            else if (name.Contains(".XX")) { successLamps += 0; }
            else if (name.Contains(".XXX")) { successLamps += 1; }
        }
        if (platformB.transform.childCount != 0)
        {
            String name = platformB.GetComponentInChildren<AudioSource>().clip.name;
            if (name.Contains(".X")) { successLamps -= 1; }
            else if (name.Contains(".XX")) { successLamps += 0; }
            else if (name.Contains(".XXX")) { successLamps += 1; }
        }
        if (platformC.transform.childCount != 0)
        {
            String name = platformC.GetComponentInChildren<AudioSource>().clip.name;
            if (name.Contains(".X")) { successLamps -= 1; }
            else if (name.Contains(".XX")) { successLamps += 0; }
            else if (name.Contains(".XXX")) { successLamps += 1; }
        }

        victoryScript.LevelOneVictory(successLamps);
    }
}
