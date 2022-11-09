using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
   

    [SerializeField] int itemCount;
    [SerializeField] string itemName;
    [SerializeField] GameObject firstPersonController;
    [SerializeField] bool itemCountAffectedByScore;
    [SerializeField] private bool destroyOnInteract = true;
    private bool alreadyPickedUp = false;
    public override void OnFocus()
    {
      
    }

    public override void OnInteract()
    {
        int adjustedItemCount = itemCount;

        //alters item pickup count dependent on player score if bool is true
        if (itemCountAffectedByScore)
        {
            float denominator = firstPersonController.GetComponent<FirstPersonController>().diffcultyValue();
            
           
            adjustedItemCount = (int)((float)adjustedItemCount / denominator);
           
        }
        if (!alreadyPickedUp && firstPersonController.GetComponent<FirstPersonController>().AddInventoryItem(itemName, adjustedItemCount))
        {
            alreadyPickedUp = true;
            if(destroyOnInteract)
                Destroy(gameObject);
        }
            
    }

    public override void OnLoseFocus()
    {

    }
}
