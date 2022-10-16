using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class OnScreenUIScript : MonoBehaviour
{


    public static bool inventoryUp = false;
    public static bool readingNote = false;

    public GameObject inventoryMenuUI;
    public GameObject ammoDisplayUI;
    public GameObject crosshairs;
    public GameObject notePanel;
    public GameObject firstPersonController;
    public TextMeshProUGUI notePanelText;

    [SerializeField] private float[] lowerHealthThreshold = new float[4];
    [SerializeField] private Color healthIndicatorColor;
    [SerializeField] private Image healthIndicator;
    [SerializeField] private int fontNumber;
    [SerializeField] private int noteNumber;
    [SerializeField] private TMP_FontAsset[] fontsForNotes;

    private string path = "Assets/Items/Menu Items/In Game Notes.txt";
    private float health;


    void Start()
    {
        health = firstPersonController.GetComponent<FirstPersonController>().getCurrentHealth();
    }
    
    void Update()
    {
        //open/close inventory
        if (Input.GetKeyDown(KeyCode.Tab) && !PauseScript.isPaused)
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
        else if (Input.GetKeyDown(KeyCode.Escape) && readingNote)
        {
                CloseNote();  
        }
        //temporary else if to test notes until we have interactable objects
        else if (Input.GetKeyDown(KeyCode.N))
        {
            if (readingNote)
            {
                CloseNote();
            }
            else
            {
                OpenNote();
            }
        }

        if (health != firstPersonController.GetComponent<FirstPersonController>().getCurrentHealth())
        {
            health = firstPersonController.GetComponent<FirstPersonController>().getCurrentHealth();
            changeHealthIndicator();
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
        if (health >= lowerHealthThreshold[0])
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
        else if(health > lowerHealthThreshold[3])
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
    #endregion

    #region Notes Functions


    //function currently will pull up the note of the value for the serialized field, noteNumber. 
    //Once we have interactable notes in the world, we will use this function as an onclick function for the interactable note and it will 
    //supply the noteNumber as an argument to the function.
    void OpenNote()
    {
        notePanel.SetActive(true);
        ammoDisplayUI.SetActive(false);
        crosshairs.SetActive(false);
        Time.timeScale = 0f;
        readingNote = true;


        StreamReader textReader = new StreamReader(path);

        string currentText = textReader.ReadLine(); 
        while(currentText.CompareTo("Text "+ noteNumber.ToString()) != 0 && !textReader.EndOfStream)
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

    private void CloseNote()
    {
        notePanel.SetActive(false);
        ammoDisplayUI.SetActive(true);
        crosshairs.SetActive(true);
        Time.timeScale = 1f;
        readingNote = false;
    }
    #endregion
}
