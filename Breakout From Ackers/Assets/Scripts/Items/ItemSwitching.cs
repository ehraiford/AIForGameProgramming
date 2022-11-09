using UnityEngine;

public class ItemSwitching : MonoBehaviour
{
    public int selectedItem = 0;
    private string currentItemName = "";
    private string[] inventoryItems;
    private string[] equippableItems = new string[4];
    private int numItems;

    private GameObject ammoUI;
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private Animator playerAnimations;

    // Start is called before the first frame update
    void Start()
    {
        ammoUI = GameObject.Find("HUD");
        inventoryItems = playerController.inventoryItems;

        //Disable all items since the player starts with an empty inventory
        foreach (Transform item in transform) item.gameObject.SetActive(false);

        // Create array of all equippable items
        setEquippableItems();
    }

    // Update is called once per frame
    void Update()
    {
        // Updates inventory
        inventoryItems = playerController.inventoryItems;

        int previousSelectedItem = selectedItem;

        // Calculates the number of equippable items and handles inputs accordingly
        numItems = calcNumEquippableItems();

        if (numItems == 0) // Player has no equippable items
        {
            selectedItem = 0;
            currentItemName = "Hands";
        }
        else if (numItems == 1) // Player has 1 equippable item
        {
            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (isItemEquippable(i)) selectedItem = i;
            }

            if (currentItemName == "Hands")
            {
                SelectItem();
            }
        }
        else // Player has multiple equippable items
        {
            HandleMouseWheelInput();
            HandleNumberInput();
        }

        if (previousSelectedItem != selectedItem) SelectItem();

        // Activate and deactivate ammo display
        if(currentItemName == "M1911" && !ammoUI.activeSelf) ammoUI.SetActive(true);
        if(currentItemName != "M1911" && ammoUI.activeSelf) ammoUI.SetActive(false);
    }

    void setEquippableItems()
    {
        equippableItems[0] = "M1911";
        equippableItems[1] = "MedKit";
        equippableItems[2] = "Pills";
        equippableItems[3] = "Knife";
    }

    int calcNumEquippableItems()
    {
        int numItems = 0;

        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if (isItemEquippable(i)) numItems++;
        }

        return numItems;
    }

    void SelectItem()
    {
        //Time time = Time.timeScale;

        foreach (Transform item in transform)
        {
            if (item.name == inventoryItems[selectedItem])
            {
                item.gameObject.SetActive(true);
                currentItemName = item.name;
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    void HandleMouseWheelInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedItem >= inventoryItems.Length - 1)
                selectedItem = 0;
            else
                selectedItem++;

            while (!isItemEquippable(selectedItem))
            {
                selectedItem++;

                if (selectedItem >= inventoryItems.Length - 1) selectedItem = 0;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedItem <= 0)
                selectedItem = 7;
            else
                selectedItem--;    

            while (!isItemEquippable(selectedItem))
            {
                selectedItem--;

                if (selectedItem <= 0) selectedItem = 7;
            }
        }
    }

    void HandleNumberInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isItemEquippable(0))
            selectedItem = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && isItemEquippable(1))
            selectedItem = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3) && isItemEquippable(2))
            selectedItem = 2;

        if (Input.GetKeyDown(KeyCode.Alpha4) && isItemEquippable(3))
            selectedItem = 3;

        if (Input.GetKeyDown(KeyCode.Alpha5) && isItemEquippable(4))
            selectedItem = 4;

        if (Input.GetKeyDown(KeyCode.Alpha6) && isItemEquippable(5))
            selectedItem = 5;

        if (Input.GetKeyDown(KeyCode.Alpha7) && isItemEquippable(6))
            selectedItem = 6;

        if (Input.GetKeyDown(KeyCode.Alpha8) && isItemEquippable(7))
            selectedItem = 7;
    }

    bool isItemEquippable(int itemNum)
    {
        return inventoryItems[itemNum] == equippableItems[0] || inventoryItems[itemNum] == equippableItems[1] || inventoryItems[itemNum] == equippableItems[2] || inventoryItems[itemNum] == equippableItems[3];
    }

    public string getCurrentItemName()
    {
        return currentItemName;
    }
}
