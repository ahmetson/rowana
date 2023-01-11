using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGrean : MonoBehaviour
{
    public SetVolume vol;
    public GameObject panelGrean;

    private void Awake()
    {
        panelGrean.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Grean");
        panelGrean.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnTriggerExit(Collider other)
    {
        panelGrean.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
