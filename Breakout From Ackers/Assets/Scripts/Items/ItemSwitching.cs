using UnityEngine;

public class ItemSwitching : MonoBehaviour
{
    public int selectedItem = 0;
    private string currentItemName = "";
    private string[] inventoryItems;

    private GameObject ammoUI;
    [SerializeField] private GameObject playerController;

    // Start is called before the first frame update
    void Start()
    {
        ammoUI = GameObject.Find("HUD");
        inventoryItems = playerController.GetComponent<FirstPersonController>().inventoryItems;

        SelectItem();
    }

    // Update is called once per frame
    void Update()
    {
        // Updates inventory
        inventoryItems = playerController.GetComponent<FirstPersonController>().inventoryItems;

        int previousSelectedItem = selectedItem;

        HandleMouseWheelInput();
        HandleNumberInput();

        if (previousSelectedItem != selectedItem) SelectItem();

        if (inventoryItems[selectedItem] == "") currentItemName = "";

        // Activate and deactivate ammo display
        if(currentItemName == "M1911" && !ammoUI.activeSelf) ammoUI.SetActive(true);
        if (currentItemName != "M1911" && ammoUI.activeSelf) ammoUI.SetActive(false);
    }

    void SelectItem()
    {
        //System.Threading.Thread.Sleep(1000);

        int currentItem = 0;

        foreach (Transform item in transform)
        {
            if (currentItem == selectedItem)
            {
                item.gameObject.SetActive(true);
                currentItemName = item.name;
            }
            else
            {
                item.gameObject.SetActive(false);
            }

            currentItem++;
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

            while (inventoryItems[selectedItem] == "")
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

            while (inventoryItems[selectedItem] == "")
            {
                selectedItem--;

                if (selectedItem <= 0) selectedItem = 7;
            }
        }
    }

    void HandleNumberInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryItems[0] != "")
            selectedItem = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && inventoryItems[1] != "")
            selectedItem = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3) && inventoryItems[2] != "")
            selectedItem = 2;

        if (Input.GetKeyDown(KeyCode.Alpha4) && inventoryItems[3] != "")
            selectedItem = 3;

        if (Input.GetKeyDown(KeyCode.Alpha5) && inventoryItems[4] != "")
            selectedItem = 4;

        if (Input.GetKeyDown(KeyCode.Alpha6) && inventoryItems[5] != "")
            selectedItem = 5;

        if (Input.GetKeyDown(KeyCode.Alpha7) && inventoryItems[6] != "")
            selectedItem = 6;

        if (Input.GetKeyDown(KeyCode.Alpha8) && inventoryItems[7] != "")
            selectedItem = 7;
    }

    public string getCurrentItemName()
    {
        return currentItemName;
    }
}
