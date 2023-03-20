using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private InputMaster controls;
    private float moveSpeed = 5f;
    private float sprintSpeed = 10f;
    private float crouchSpeed = 3f;
    private Vector3 velosity;
    private float gravity = -9.81f;
    private Vector2 move;
    private float jumpHeight = 2.4f;
    private CharacterController controller;

    public Transform ground;
    public float distanceToGround = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    private AudioSource stepSource;
    public List<AudioClip> stepsSounds;
    public List<AudioClip> sprintSounds;
    

    void Awake()
    {
        controls = new InputMaster();
        controller = GetComponent<CharacterController>();

        stepSource = GetComponentInChildren<AudioSource>();
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            move = controls.Player.Movement.ReadValue<Vector2>();

            Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
            controller.Move(movement * sprintSpeed * Time.deltaTime);
            Invoke("StepsSound", 0.3f);

        } 
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            move = controls.Player.Movement.ReadValue<Vector2>();

            Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
            controller.Move(movement * crouchSpeed * Time.deltaTime);
        } 
        else 
        {
            move = controls.Player.Movement.ReadValue<Vector2>();

            Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
            controller.Move(movement * moveSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D))
            {
                Invoke("StepsSound", 0.5f);
            }
        }
    }

    private void StepsSound()
    {
        
        stepSource.clip = stepsSounds[Random.Range(0, stepsSounds.Count)];
        stepSource.Play();
        CancelInvoke();
    }

    private void Jump() 
    {
        if(controls.Player.Jump.triggered && isGrounded) {
            velosity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            CancelInvoke();
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
