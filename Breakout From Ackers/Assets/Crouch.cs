using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    CharacterController playerController;
    Transform playerTransform;
    float originalHeight;
    public float reducdedHeight;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();
        originalHeight = playerController.height;
    }

    // Update is called once per frame
    void Update()
    {
        // Detect Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CrouchDown();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StandUp();
        }
    }

    // Function to reduce height
    void CrouchDown()
    {
        playerController.height = reducdedHeight;
    }

    // Function to reset height
    void StandUp()
    {
        playerController.height = originalHeight;
    }
}
