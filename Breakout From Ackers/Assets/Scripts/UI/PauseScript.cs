using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject onScreenUI;
    public GameObject crosshairs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !OnScreenUIScript.inventoryUp && !OnScreenUIScript.readingNote)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        crosshairs.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
       
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        onScreenUI.SetActive(true);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        crosshairs.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.visible = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        onScreenUI.SetActive(false);
    }


    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
