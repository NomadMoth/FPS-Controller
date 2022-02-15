using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    //Checks
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //Movement
    public float crouchSpeed = 8f;
    public float moveSpeed = 15f;
    public float sprintSpeed = 12f;
    private float currentSpeed;

    //Gravity and Jumping
    private float currentGravity;
    public float gravity = -9f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    bool isGrounded;

    //Crouching
    bool isCrouching;
    float standingHeight = 2f;
    public float crouchHeight = 1f;

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sprinting();
        Jumping();
        Gravity();
        Crouching();
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
    }

    void Jumping()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = moveSpeed + sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }

    void Crouching()
    {
        
    }
}
