 using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    private InputMaster controls;
    [HideInInspector] public float mouseSensitivity;
    private Vector2 mouseLook;
    private Camera cam;
    public float xRotation = 0f;
    public Transform playerBody;
    public Transform playerArms;
    float mouseX;
    float mouseY;

    private Vector3 orignalCameraPos;
    public float shakeAmount = 0.2f;

    void Awake()
    {
        playerBody = transform.parent;
        cam = playerBody.GetComponentInChildren<Camera>();

        controls = new InputMaster();
        Cursor.lockState = CursorLockMode.Locked;

        orignalCameraPos = cam.transform.localPosition;
    }

    private void Update()
    {
        Look();
        StartCoroutine(WaitForFunction());
        
    }
    private IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(1f);
        playerArms.localRotation = Quaternion.Euler( mouseY, -mouseX, 0f);
    }

    private void Look()
    {
        mouseLook = controls.Player.Look.ReadValue<Vector2>();

        mouseX = mouseLook.x * mouseSensitivity * Time.deltaTime;
        mouseY = mouseLook.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);

        controls.Player.Movement.ReadValue<Vector2>();

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();  
    }
}