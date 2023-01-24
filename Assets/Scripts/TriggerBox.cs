using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TriggerBox : MonoBehaviour
{
    public SetMusicSounds set;
    public GameObject panelMenuTool;
    public GameObject panelPushE;
    public MouseLook cameraMove;
    public InputMaster controls;
    private bool pushE;
    public bool onTrigger;

    private void Awake()
    {
        panelMenuTool.SetActive(false);
        panelPushE.SetActive(false);
        controls = new InputMaster();
        pushE = false;
    }

    private void Update()
    {
        if (pushE)
        {
            Action();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        pushE = true;
        panelPushE.SetActive(true);
        onTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        panelMenuTool.SetActive(false);
        panelPushE.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        pushE = false;
        cameraMove.mouseSensitivity = 50f;
        onTrigger = false;
    }

    public void Action()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            panelPushE.SetActive(false);
            panelMenuTool.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            cameraMove.mouseSensitivity = 0f;
        }
    }
}