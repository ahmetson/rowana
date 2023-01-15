using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TriggerYellow : MonoBehaviour
{
    public SetMusicSounds set;
    public GameObject panelYellow;
    public GameObject panelCanUI;
    public MouseLook cameraMove;
    public InputMaster controls;
    public bool canUI;

    private void Awake()
    {
        Debug.Log("Start");
        panelYellow.SetActive(false);
        panelCanUI.SetActive(false);
        controls = new InputMaster();
        canUI = false;
    }

    private void Update()
    {
        if (canUI)
        {
            Action();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Yellow");
        canUI = true;
        panelCanUI.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        panelYellow.SetActive(false);
        panelCanUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        canUI = false;
        cameraMove.mouseSensitivity = 50f;
    }

    public void Action()
    {
        if (Input.GetKeyDown(KeyCode.E) )
        {
            panelCanUI.SetActive(false);
            panelYellow.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            cameraMove.mouseSensitivity = 0f;
        } 
    }
}
