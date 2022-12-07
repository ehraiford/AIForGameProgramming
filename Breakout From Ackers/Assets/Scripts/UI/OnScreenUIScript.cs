using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;

public class OnScreenUIScript : MonoBehaviour
{


    public static bool inventoryUp;
    public static bool readingNote;
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject onScreenUI;
    public GameObject inventoryMenuUI;
    public GameObject ammoDisplayUI;
    public GameObject crosshairs;
    public GameObject notePanel;
    public GameObject firstPersonController;
    [SerializeField] GameObject backgroundMusic;
    [SerializeField] GameObject bossAudio;


    public TextMeshProUGUI notePanelText;

    [Header("Inventory Screen Stuff")]
    [SerializeField] private TextMeshProUGUI[] inventoryTempText;
    [SerializeField] private Image[] inventoryImages;
    [SerializeField] private Sprite[] imageSources;
    [SerializeField] private float[] lowerHealthThreshold = new float[4];
    [SerializeField] private Color healthIndicatorColor;
    [SerializeField] private Image healthIndicator;
    public TextMeshProUGUI currentRoom;
    [SerializeField] private TMP_FontAsset[] fontsForNotes;

    [Header("Objective Stuff")]
    public TextMeshProUGUI objective;
    [SerializeField] private string[] objectiveList;

    [Header("Heads Up Stuff")]
    [SerializeField] private TextMeshProUGUI headsUpText;
    [SerializeField] float fadeInTime, stayTime, fadeOutTime;
    public static bool fadeHeadsUp = false;
    private float fadeTimer;




    private string path = "Assets/Items/Menu Items/In Game Notes.txt";
    private float health;


    void Start()
    {
        inventoryUp = false;
        readingNote = false;
        health = firstPersonController.GetComponent<FirstPersonController>().GetCurrentHealth();

        SetCurrentObjective(0);
        currentRoom.text = "Temporary Hospital";

    }

    void Update()
    {
        //open/close inventory
        if (Input.GetKeyDown(KeyCode.Tab) && !isPaused && !readingNote && !firstPersonController.GetComponent<FirstPersonController>().isDead)
        {
            if (inventoryUp)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
        //close note
        else if (Input.GetKeyDown(KeyCode.Escape) && !inventoryUp && !readingNote && !firstPersonController.GetComponent<FirstPersonController>().isDead)
        {
            if (!isPaused)
            {
                Pause();
            }
        }

        //changes health indicator if it isn't accurate
        if (inventoryUp)
        {
            health = firstPersonController.GetComponent<FirstPersonController>().GetCurrentHealth();
            changeHealthIndicator();
        }

        //handles fade in / out of headsUp text
        if (fadeHeadsUp)
        {
            if (Time.time - fadeTimer <= fadeInTime)
            {
                float alpha = (Time.time - fadeTimer) / fadeInTime;
                headsUpText.color = new Color(headsUpText.color.r, headsUpText.color.g, headsUpText.color.b, alpha);
            }
            else if (Time.time - fadeTimer <= fadeInTime + stayTime)
            {
                headsUpText.color = new Color(headsUpText.color.r, headsUpText.color.g, headsUpText.color.b, 1);
            }
            else if (Time.time - fadeTimer <= fadeInTime + stayTime + fadeOutTime)
            {
                float alpha = 1 - (Time.time - fadeTimer - fadeInTime - stayTime) / fadeOutTime;

                headsUpText.color = new Color(headsUpText.color.r, headsUpText.color.g, headsUpText.color.b, alpha);
            }
            else
            {
                headsUpText.color = new Color(headsUpText.color.r, headsUpText.color.g, headsUpText.color.b, 0.0f);

                fadeHeadsUp = false;
            }
        }
    }

    #region Inventory Functions 
    private void OpenInventory()
    {
        inventoryMenuUI.SetActive(true);
        ammoDisplayUI.SetActive(false);
        crosshairs.SetActive(false);
        Time.timeScale = 0f;
        inventoryUp = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        DisplayInventoryItems();
    }

    private void DisplayInventoryItems()
    {

        for (int i = 0; i < 8; i++)
        {
            inventoryTempText[i].text = firstPersonController.GetComponent<FirstPersonController>().inventoryItems[i];

            if (inventoryTempText[i].text.CompareTo("M1911") == 0)
            {
                //inventoryImages[i].color = new Color(inventoryImages[i].color.r, inventoryImages[i].color.g, inventoryImages[i].color.b, 1);
                inventoryImages[i].sprite = imageSources[0];

            }
            else if (inventoryTempText[i].text.CompareTo("Pistol Ammo") == 0)
            {
                inventoryImages[i].sprite = imageSources[1];
                //  inventoryImages[i].color = new Color(inventoryImages[i].color.r, inventoryImages[i].color.g, inventoryImages[i].color.b, 0);
            }
            else if (inventoryTempText[i].text.CompareTo("MedKit") == 0)
            {
                inventoryImages[i].sprite = imageSources[2];
                //  inventoryImages[i].color = new Color(inventoryImages[i].color.r, inventoryImages[i].color.g, inventoryImages[i].color.b, 0);
            }
            else if (inventoryTempText[i].text.CompareTo("Blue Mass Pills") == 0)
            {
                //inventoryImages[i].color = new Color(inventoryImages[i].color.r, inventoryImages[i].color.g, inventoryImages[i].color.b, 0);
                inventoryImages[i].sprite = imageSources[3];

            }
            else if (inventoryTempText[i].text.Contains("Key"))
            {
                inventoryImages[i].sprite = imageSources[4];
                //  inventoryImages[i].color = new Color(inventoryImages[i].color.r, inventoryImages[i].color.g, inventoryImages[i].color.b, 0);
            }
            else if (inventoryTempText[i].text.CompareTo("Fish") == 0)
            {
                inventoryImages[i].sprite = imageSources[5];
                // inventoryImages[i].color = new Color(inventoryImages[i].color.r, inventoryImages[i].color.g, inventoryImages[i].color.b, 0);
            }
            else if (inventoryTempText[i].text.CompareTo("Pure Blue Mass") == 0)
            {
                inventoryImages[i].sprite = imageSources[6];
                //    inventoryImages[i].color = new Color(inventoryImages[i].color.r, inventoryImages[i].color.g, inventoryImages[i].color.b, 0);
            }
            else if (inventoryTempText[i].text.CompareTo("Blue Mass Ammo") == 0)
            {
                inventoryImages[i].sprite = imageSources[7];
                // inventoryImages[i].color = new Color(inventoryImages[i].color.r, inventoryImages[i].color.g, inventoryImages[i].color.b, 0);
            }
            else if (inventoryTempText[i].text.CompareTo("Knife") == 0)
            {
                inventoryImages[i].sprite = imageSources[8];
            }
            else
            {
                inventoryImages[i].sprite = imageSources[9];
            }

            if (inventoryTempText[i].text.Contains("Ammo"))
            {
                inventoryTempText[i].text += " (" + firstPersonController.GetComponent<FirstPersonController>().inventoryItemsCount[i] + ")";
            }
        }

    }

    private void CloseInventory()
    {
        inventoryMenuUI.SetActive(false);
        ammoDisplayUI.SetActive(true);
        crosshairs.SetActive(true);
        Time.timeScale = 1f;
        inventoryUp = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void changeHealthIndicator()
    {
        if (firstPersonController.GetComponent<FirstPersonController>().isDebuffed)
        {
            healthIndicatorColor.r = 0.1961f;
            healthIndicatorColor.g = 0.098f;
            healthIndicatorColor.b = 0.1961f;
        }
        else if (health >= lowerHealthThreshold[0])
        {
            healthIndicatorColor.r = 0.1768868f;
            healthIndicatorColor.g = 0.5f;
            healthIndicatorColor.b = 0.2438213f;
        }
        else if (health > lowerHealthThreshold[1])
        {
            healthIndicatorColor.r = 0.3067373f;
            healthIndicatorColor.g = 0.5754717f;
            healthIndicatorColor.b = 0.414604f;
        }
        else if (health > lowerHealthThreshold[2])
        {
            healthIndicatorColor.r = 0.5019608f;
            healthIndicatorColor.g = 0.4325789f;
            healthIndicatorColor.b = 0.1764706f;
        }
        else if (health > lowerHealthThreshold[3])
        {
            healthIndicatorColor.r = 0.5019608f;
            healthIndicatorColor.g = 0.3170498f;
            healthIndicatorColor.b = 0.1764706f;
        }
        else
        {
            healthIndicatorColor.r = 0.5019608f;
            healthIndicatorColor.g = 0.1764706f;
            healthIndicatorColor.b = 0.1793361f;
        }
        healthIndicatorColor.a = 1f;
        healthIndicator.color = healthIndicatorColor;

    }

    public void SetCurrentObjective(int objectiveNumber)
    {
        objective.text = objectiveList[objectiveNumber];
    }
    #endregion

    #region Notes Functions

    public void OpenNote(int noteNumber, int fontNumber)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        readingNote = true;
        notePanel.SetActive(true);
        ammoDisplayUI.SetActive(false);
        crosshairs.SetActive(false);
        Time.timeScale = 0f;



        StreamReader textReader = new StreamReader(path);

        string currentText = textReader.ReadLine();
        while (currentText.CompareTo("Text " + noteNumber.ToString()) != 0 && !textReader.EndOfStream)
        {
            currentText = textReader.ReadLine();
        }

        currentText = "";
        string addOn;
        while (textReader.Peek() != 42 && !textReader.EndOfStream)
        {
            addOn = textReader.ReadLine();

            currentText = currentText + addOn + "\n";

        }
        notePanelText.text = currentText;
        notePanelText.font = fontsForNotes[fontNumber];
        textReader.Close();
    }

    public void CloseNote()
    {
        readingNote = false;
        notePanel.SetActive(false);
        ammoDisplayUI.SetActive(true);
        crosshairs.SetActive(true);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    #endregion

    #region Pause Menu
    public void Resume()
    {
        backgroundMusic.GetComponent<BGMScript>().Unpause();
        if(bossAudio != null)
        {
            bossAudio.GetComponent<BossStat>().resumeFootStep();
        }
        
        pauseMenuUI.SetActive(false);
        crosshairs.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;

        onScreenUI.SetActive(true);
    }

    void Pause()
    {
        backgroundMusic.GetComponent<BGMScript>().PauseMusic();
        if(bossAudio != null)
        {
            bossAudio.GetComponent<BossStat>().pauseFootStep();
        }
        pauseMenuUI.SetActive(true);
        crosshairs.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        onScreenUI.SetActive(false);
    }


    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }


    #endregion

    #region HeadsUpDisplay
    public void SetHeadsUpText(string newHeadsUpText)
    {
        headsUpText.text = newHeadsUpText;
        fadeHeadsUp = true;
        fadeTimer = Time.time;
    }
    #endregion
}
