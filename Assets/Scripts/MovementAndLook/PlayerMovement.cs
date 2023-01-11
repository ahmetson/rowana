using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private InputMaster controls;
    private float moveSpeed = 6f;
    private Vector3 velosity;
    private float gravity = -9.81f;
    private Vector2 move;
    private float jumpHeight = 2.4f;
    private CharacterController controller;

    public Transform ground;
    public float distanceToGround = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    void Awake()
    {
        controls = new InputMaster();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Grav();
        PlayerMovements();
        Jump();
    }

    private void Grav()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);

        if(isGrounded && velosity.y < 0) {
            velosity.y = -2f; 
        }

        velosity.y += gravity * Time.deltaTime;
        controller.Move(velosity * Time.deltaTime);
    }

    void PlayerMovements()
    {
        move = controls.Player.Movement.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void Jump() 
    {
        if(controls.Player.Jump.triggered && isGrounded) {
            velosity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
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
