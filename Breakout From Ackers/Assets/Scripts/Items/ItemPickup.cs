using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
   

    [SerializeField] int itemCount;
    [SerializeField] string itemName;
    [SerializeField] GameObject firstPersonController;
    [SerializeField] bool itemCountAffectedByScore;
    public override void OnFocus()
    {
      
    }

    public override void OnInteract()
    {
        int adjustedItemCount = itemCount;

        if (itemCountAffectedByScore)
        {
            float denominator = firstPersonController.GetComponent<FirstPersonController>().diffcultyValue();
            //denominator = denominator * denominator;
            print("Denominator " + denominator);
            adjustedItemCount = (int)((float)adjustedItemCount / denominator);
            print("Added item count" + adjustedItemCount);
        }
        if (firstPersonController.GetComponent<FirstPersonController>().AddInventoryItem(itemName, adjustedItemCount))
            Destroy(gameObject);
    }

    public override void OnLoseFocus()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
