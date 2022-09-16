using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -19.62f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        
        // Checks if the player is in contact with the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // If they are, velocity is reset to 0
        // In this case I am using a small negative number to make sure they are on the ground
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Takes input from Unity
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Uses transform since movement is relative to the player and not global
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // If the jump key is pressed while the player is on the ground, they are given an upwards velocity
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
