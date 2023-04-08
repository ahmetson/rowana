using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        platformA = GameObject.FindWithTag("PlatformA");
        platformB = GameObject.FindWithTag("PlatformB");
        platformC = GameObject.FindWithTag("PlatformC");   
        
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
            List<AudioSource> sources = new List<AudioSource>();
            sources.AddRange(platformA.GetComponentsInChildren<AudioSource>());
            foreach (AudioSource name in sources)
            {
                if (name.CompareTag("musicA"))
                {
                    if (name.clip.name.Contains(".negatively")) { successLamps -= 1; }
                    else if (name.clip.name.Contains(".neutral")) { successLamps += 0; }
                    else if (name.clip.name.Contains(".positively")) { successLamps += 1; }
                }
            }
        }
        if (platformB.transform.childCount != 0)
        {
            List<AudioSource> sources = new List<AudioSource>();
            sources.AddRange(platformB.GetComponentsInChildren<AudioSource>());
            foreach (AudioSource name in sources)
            {
                if (name.CompareTag("musicB"))
                {
                    if (name.clip.name.Contains(".negatively")) { successLamps -= 1; }
                    else if (name.clip.name.Contains(".neutral")) { successLamps += 0; }
                    else if (name.clip.name.Contains(".positively")) { successLamps += 1; }
                }
            }
        }
        if (platformC.transform.childCount != 0)
        {
            List<AudioSource> sources = new List<AudioSource>();
            sources.AddRange(platformC.GetComponentsInChildren<AudioSource>());
            foreach (AudioSource name in sources)
            {
                if (name.CompareTag("musicC"))
                {
                    if (name.clip.name.Contains(".negatively")) { successLamps -= 1; }
                    else if (name.clip.name.Contains(".neutral")) { successLamps += 0; }
                    else if (name.clip.name.Contains(".positively")) { successLamps += 1; }
                }
            }
            /*
            GameObject crystal = GameObject.FindWithTag("musicC");
            String name = crystal.GetComponent<AudioSource>().clip.name;
            Debug.Log(name);
            if (name.Contains(".negatively")) { successLamps -= 1; }
            else if (name.Contains(".neutral")) { successLamps += 0; }
            else if (name.Contains(".positively")) { successLamps += 1; }
            */
        }

        victoryScript.LevelOneVictory(successLamps);
    }
}
