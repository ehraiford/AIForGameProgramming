using UnityEngine;
using System.Collections;

public class ItemSwitching : MonoBehaviour
{
    public int selectedItem = 0;
    private string currentItemName = "";
    private string[] inventoryItems;
    private string[] equippableItems = new string[4];
    private int numItems;
    public bool isSwitching = false;
    private int numScrollInput = 0;

    [Header("Object References")]
    [SerializeField] private GameObject ammoUI;
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private Animator playerAnimations;

    [Header("Item References")]
    [SerializeField] private GameObject m1911;
    [SerializeField] private GameObject medKit;
    [SerializeField] private GameObject pills;
    [SerializeField] private GameObject knife;

    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = playerController.inventoryItems;

        //Disable all items since the player starts with an empty inventory
        foreach (Transform item in transform) item.gameObject.SetActive(false);

        // Create array of all equippable items
        setEquippableItems();

        // Start player with empty hands
        selectedItem = 0;
        currentItemName = "Hands";

        StartCoroutine(SelectItem());
    }

    // Update is called once per frame
    void Update()
    {
        if (numScrollInput > 0) playerAnimations.SetBool("Switching", true);
        else playerAnimations.SetBool("Switching", false);

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
                StartCoroutine(SelectItem());
            }
        }
        else // Player has multiple equippable items
        {
            // Doesn't allow the player to swap if they are in the middle of using an item
            if(!(m1911.GetComponent<Gun>().isShooting || m1911.GetComponent<Gun>().isReloading || medKit.GetComponent<MedKit>().isHealing || pills.GetComponent<Pills>().isPopping)) // || knife.GetComponent<Knife>().isAttacking
            {
                if(numScrollInput <= 8) HandleMouseWheelInput();
                if(!isSwitching) HandleNumberInput();
            }
        }

        if (previousSelectedItem != selectedItem) StartCoroutine(SelectItem());

        // Activate and deactivate ammo display
        if (currentItemName == "M1911" && !ammoUI.activeSelf) ammoUI.SetActive(true);
        if(currentItemName != "M1911" && ammoUI.activeSelf) ammoUI.SetActive(false);
    }

    private void setEquippableItems()
    {
        equippableItems[0] = "M1911";
        equippableItems[1] = "MedKit";
        equippableItems[2] = "Blue Mass Pills";
        equippableItems[3] = "Knife";
    }

    private int calcNumEquippableItems()
    {
        int numItems = 0;

        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if (isItemEquippable(i)) numItems++;
        }

        return numItems;
    }

    private IEnumerator SelectItem()
    {
        isSwitching = true;
        numScrollInput++;

        // Updates the current item's name to the new item
        foreach (Transform item in transform)
        {
            if (item.name == inventoryItems[selectedItem])
            {
                currentItemName = item.name;
            }
        }

        // Waits for the hands to go all the way down
        yield return new WaitForSeconds(0.5f);
        numScrollInput--;

        // Activates the new item and deactiveates the old one
        foreach (Transform item in transform)
        {
            if (item.name == inventoryItems[selectedItem])
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }

        // Waits for the hands to go all the way up
        yield return new WaitForSeconds(0.8f);
       
        isSwitching = false; 
    }

    private void HandleMouseWheelInput()
    {
        // Handles mousewheele up
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            isSwitching = true;

            // Moves up through the inventory and loops back to the begining if necessary
            if (selectedItem >= inventoryItems.Length - 1)
                selectedItem = 0;
            else
                selectedItem++;

            // If the current inventory item isn't equippable, skip to the next equippable item and loop back to the start if necessary
            while (!isItemEquippable(selectedItem))
            {
                selectedItem++;

                if (selectedItem >= inventoryItems.Length - 1) selectedItem = 0;
            }
        }

        // Handles mousewheel down
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // Moves down through the inventory and loops back to the end if necessary
            if (selectedItem <= 0)
                selectedItem = 7;
            else
                selectedItem--;    

            // If the current inventory item isn't equippable, skip to the next equippable item and loop back to the end if necessary
            while (!isItemEquippable(selectedItem))
            {
                selectedItem--;

                if (selectedItem <= 0) selectedItem = 7;
            }
        }
    }

    private void HandleNumberInput()
    {
        // Equips the item depending on the key press only if its equippable

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

    public void NoRemaingingItemsFindNext()
    {
        int maxCheck = 0;

        // Search for next equippable item
        while (!isItemEquippable(selectedItem))
        {
            if (maxCheck > inventoryItems.Length) break; // If there are no remaining items stop searching

            selectedItem++;

            if (selectedItem >= inventoryItems.Length - 1) selectedItem = 0;

            maxCheck++;
        }

        // Equip the new item
        StartCoroutine(SelectItem());
    }

    private bool isItemEquippable(int itemNum)
    {
        // Checks if the item in the givern inventory space matches the name of an equippable item
        return inventoryItems[itemNum] == equippableItems[0] || inventoryItems[itemNum] == equippableItems[1] || inventoryItems[itemNum] == equippableItems[2] || inventoryItems[itemNum] == equippableItems[3];
    }

    public string getCurrentItemName()
    {
        return currentItemName;
    }
}
