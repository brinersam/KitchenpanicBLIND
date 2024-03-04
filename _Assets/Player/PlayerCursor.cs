using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public IInteractable cursorSelection = null;
    
    [SerializeField] private ItemDisplayer itemDisplayer;
    [SerializeField] private PlayerController plCntrl;
    private IHoldable inventory;

    public IHoldable Inventory 
    {
        get {return inventory;}
        private set
        {
            if (value == null || value.type == ItemType.NotSet_Err)
                value = null;

            inventory = value;
            itemDisplayer.UpdateVisuals(inventory);
        }
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        Target(other);
    }

    private void OnTriggerExit(Collider other)
    {
        UnTarget(other);
    }

    public void Interact(bool alt)
    {
        if (cursorSelection == null) return;
        
        cursorSelection.Interact(this, alt);
    }
    
    private void Target(Collider other)
    {
        if (!other.TryGetComponent(out IsInteractable obj)) return;
        obj.ToggleHighlight(true);
        cursorSelection = other.GetComponent<IsInteractable>();
    }

    private void UnTarget(Collider other)
    {
        if (!other.TryGetComponent(out IsInteractable obj)) return;
        obj.ToggleHighlight(false);
        cursorSelection = null;
    }

    //separate into inventory class todo

    internal void ReceiveItem(IHoldable itemToReceive)
    {
        if (itemToReceive == null)
            Debug.LogError($"Received NULL item");

        Inventory = itemToReceive;
        //logic
    }

    public IHoldable LoseItem()
    {
        var temp = Inventory;
        Inventory = null;
        return temp;
    }
}
