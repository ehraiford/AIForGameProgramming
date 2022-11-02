using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    private float timePassed;
    [SerializeField] GameObject firstText;
    [SerializeField] GameObject secondText;
    // Start is called before the first frame update
    private void Awake()
    {
        timePassed = Time.realtimeSinceStartup;
        Time.timeScale = 0.0f;
        firstText.SetActive(false);
        secondText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - timePassed > 5 && Time.realtimeSinceStartup - timePassed < 10)
        {
            firstText.SetActive(true);
        }
        else if (Time.realtimeSinceStartup - timePassed >= 10 && Time.realtimeSinceStartup - timePassed < 15)
        {
            secondText.SetActive(true);
        }
        else if (Time.realtimeSinceStartup - timePassed >= 15)
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);

            //Logan, if you want to move the player character or reset their health or anything through the script, this would be the spot to do it.
        }
    }
}
