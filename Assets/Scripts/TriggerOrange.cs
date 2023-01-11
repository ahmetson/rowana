using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOrange : MonoBehaviour
{
    public SetVolume vol;
    public GameObject panelOrange;

    private void Awake()
    {
        panelOrange.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Orange");
        panelOrange.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnTriggerExit(Collider other)
    {
        panelOrange.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
