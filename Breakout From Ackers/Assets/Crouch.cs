using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    private bool crouching;

    [Range(0, 1.0f)]
    [SerializeField] private float crouchSpeed = 0.3f;
    [SerializeField] private float standingHeight = 3.0f;
    [SerializeField] private float crouchingHeight = 1.5f;

    [SerializeField] private Transform playerCamera = null;
    private CharacterController playerController = null;

    // Start is called before the first frame update
    void Start()
    {
       //playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect Crouch
        //crouching = Input.GetKey(KeyCode.LeftControl);
    }

    private void FixedUpdate()
    {
        /*
        var desiredHeight = crouching ? crouchingHeight : standingHeight;

        if (playerController.height != desiredHeight)
        {
            ChangeHeight(desiredHeight);

            var cameraPos = playerCamera.transform.position;
            cameraPos.y = playerController.height;

            playerCamera.transform.position = cameraPos;
        }
        */
    }

    /*
    // Function to reduce height
    private void ChangeHeight(float height)
    {
        float center = crouchingHeight / 2;

        playerController.height = Mathf.Lerp(playerController.height, height, crouchSpeed);
        playerController.center = Vector3.Lerp(playerController.center, new Vector3(0, center, 0), crouchSpeed);
    }
    */
}

/*
 [SerializeField] private float standingHeight = 3.0f;
        [SerializeField] private float crouchHeight = 1.5f;
        [SerializeField] private float timeToCrouch = 0.25f;
        [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
        [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
        private bool isCrouching;
        private bool duringCrouch;

   // Detect Crouch
            if (Input.GetKey(KeyCode.LeftControl) && !duringCrouch && m_CharacterController.isGrounded)
            {
                StartCoroutine(CrouchStand());
            } 
 
 private System.Collections.IEnumerator CrouchStand()
        {
            if(isCrouching && Physics.Raycast(m_Camera.transform.position, Vector3.up, 1f))
            {
                yield break;
            }

            duringCrouch = true;

            float timeElapsed = 0;
            float targetHeight = isCrouching ? standingHeight : crouchHeight;
            float currentHeight = m_CharacterController.height;
            Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
            Vector3 currentCenter = m_CharacterController.center;

            while(timeElapsed < timeToCrouch)
            {
                m_CharacterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed/timeToCrouch);
                m_CharacterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed/timeToCrouch);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            m_CharacterController.height = targetHeight;
            m_CharacterController.center = targetCenter;

            isCrouching = !isCrouching;

            duringCrouch = false;
        }
 */