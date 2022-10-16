using UnityEngine;

public class ItemSwitching : MonoBehaviour
{
    public int selectedItem = 0;
    private string currentItemName = "";

    // Start is called before the first frame update
    void Start()
    {
        SelectItem();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedItem = selectedItem;

        HandleMouseWheelInput();
        HandleNumberInput();

        if (previousSelectedItem != selectedItem)
            SelectItem();
    }

    void SelectItem()
    {
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
            if (selectedItem >= transform.childCount - 1)
                selectedItem = 0;
            else
                selectedItem++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedItem <= transform.childCount - 1)
                selectedItem = 0;
            else
                selectedItem--;
        }
    }

    void HandleNumberInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 1)
            selectedItem = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            selectedItem = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            selectedItem = 2;
    }

    public string getCurrentItemName()
    {
        return currentItemName;
    }
}
