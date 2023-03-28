using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public GameObject platformA;
    public GameObject platformB;
    public GameObject platformC;

    public AudioClip clipA;
    public AudioClip clipB;
    public AudioClip clipC;

    public GameObject victoryUI;

    private void Start()
    {
        victoryUI.SetActive(false);
    }
    private void Update()
    {

        if (platformA.GetComponentInChildren<AudioSource>().clip == clipA & platformB.GetComponentInChildren<AudioSource>().clip == clipB & platformC.GetComponentInChildren<AudioSource>().clip == clipC)
        {
            victoryUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }
}
