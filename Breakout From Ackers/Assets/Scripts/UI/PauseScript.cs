using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject onScreenUI;
    public GameObject firstPersonController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !OnScreenUIScript.inventoryUp)
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
        Time.timeScale = 1f;
        isPaused = false;
       
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        onScreenUI.SetActive(true);
        firstPersonController.SetActive(true);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.visible = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        onScreenUI.SetActive(false);
        firstPersonController.SetActive(false);
    }


    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
