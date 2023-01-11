using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlue : MonoBehaviour
{
    public SetVolume vol;
    public GameObject panelBlue;

    private void Awake()
    {
        panelBlue.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Blue");
        panelBlue.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnTriggerExit(Collider other)
    {
        panelBlue.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
