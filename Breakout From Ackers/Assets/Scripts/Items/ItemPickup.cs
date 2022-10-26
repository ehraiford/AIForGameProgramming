using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private GameObject player;
    [SerializeField] private string itemName;
    [SerializeField] private int itemCount;

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        bool itemPickedUp = player.GetComponent<FirstPersonController>().AddInventoryItem(itemName, itemCount);

        if (itemPickedUp) Destroy(gameObject);
    }

    public override void OnLoseFocus()
    {

    }
}
