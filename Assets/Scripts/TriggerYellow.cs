using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerYellow : MonoBehaviour
{
    public SetVolume vol;
    public GameObject panelYellow;

    private void Awake()
    {
        Debug.Log("Start");
        panelYellow.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Yellow");
        panelYellow.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnTriggerExit(Collider other)
    {
        panelYellow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
