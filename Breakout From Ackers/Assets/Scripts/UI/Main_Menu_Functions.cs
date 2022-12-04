using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Functions : MonoBehaviour
{

    [SerializeField] GameObject mainMenu, controlsMenu;

    public void PlayGame()
    {
        
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {

        Debug.Log("Quit Button Pressed.");
        Application.Quit();
    }

    public void ViewControls()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void ExitControls()
    {
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}