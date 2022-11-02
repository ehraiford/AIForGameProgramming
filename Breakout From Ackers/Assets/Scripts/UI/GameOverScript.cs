using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    private float timePassed;
    [SerializeField] TextMeshProUGUI firstText;
    [SerializeField] TextMeshProUGUI secondText;
    [SerializeField] AudioSource audioSource;
    

    //freezes time, starts the audio, and starts tracking time passed.
    private void Awake()
    {
        timePassed = Time.realtimeSinceStartup;
        Time.timeScale = 0.0f;

        firstText.color = new Color(0, 0, 0, 255);
        secondText.color = new Color(0, 0, 0, 255);


        audioSource.Play();
    }

    void Update()
    {
       
        //switch case handles the opacity changes of the text and then restarts time and removes the death screen after 18 seconds
        switch(Time.realtimeSinceStartup - timePassed)
        {
            case  <3:
                break;
            case <10:
                ChangeAlpha(firstText, 0.002f);
                break;
            case <18:
                ChangeAlpha(secondText, 0.002f);
                break;
            default:
                Time.timeScale = 1.0f;
                gameObject.SetActive(false);

                //Logan, if you want to move the player character or reset their health or anything through the script, this would be the spot to do it.
                break;
        }
       
    }

    //Adds or Subtracts the alpha difference from the text.
    private void ChangeAlpha(TextMeshProUGUI text, float alphaChange)
    {
        text.color = new Color(1, 1, 1, text.faceColor.a + alphaChange);
        Debug.Log("New Alpha:" + text.color.a);
    }
}
