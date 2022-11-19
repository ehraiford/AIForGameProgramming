using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public float timePassed;
    [SerializeField] TextMeshProUGUI firstText;
    [SerializeField] TextMeshProUGUI secondText;
    [SerializeField] AudioSource audioSource;
    private float firstAlpha, secondAlpha;
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private GameObject respawnPoint;

    //freezes time, starts the audio, and starts tracking time passed.
    private void Awake()
    {
        firstText.color = new Color(0, 0, 0, 0);
        secondText.color = new Color(0, 0, 0, 0);

        audioSource.Play();
    }

    void Update()
    {
        if (firstAlpha < 0f)
            firstAlpha = 0f;
        if (secondAlpha < 0f)
            secondAlpha = 0f;

        if (firstAlpha > 1.0f)
            firstAlpha = 1.0f;
        if (secondAlpha > 1.0f)
            secondAlpha = 1.0f;


        //switch case handles the opacity changes of the text and then restarts time and removes the death screen after 18 seconds
        switch(Time.realtimeSinceStartup - timePassed)
        {
            case  <2f:
                break;
            case <8:
                firstAlpha += 0.002f;
                firstText.color = new Color(255, 255, 255, firstAlpha);
                
                break;
            case <9:
                secondAlpha += 0.002f;
                secondText.color = new Color(255, 255, 255, secondAlpha);
                break;
            case < 13:
                firstAlpha -= 0.003f;
                firstText.color = new Color(255, 255, 255, firstAlpha);
                secondAlpha += 0.002f;
                secondText.color = new Color(255, 255, 255, secondAlpha);
                break;
            case < 18:
                secondAlpha -= 0.004f;
                secondText.color = new Color(255, 255, 255, secondAlpha);
                break;
            default:

                // Respawns the player
                // TODO: Respawn in correct place depending on scene
                playerController.AddHealth(50);
                playerController.enabled = false;
                playerController.transform.position = respawnPoint.transform.position;
                // TODO: Set rotations in final map
                Physics.SyncTransforms();
                playerController.enabled = true;
                Time.timeScale = 1.0f;
                playerController.isDead = false;
                gameObject.SetActive(false);

                //Logan, if you want to move the player character or reset their health or anything through the script, this would be the spot to do it. 

                break;
        }
        
    }
}
