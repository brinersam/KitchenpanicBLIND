using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ItemDisplayer))]
[RequireComponent (typeof(Inventory))]
public class PlayerCursor : MonoBehaviour, IInventory
{
    private IInteractable cursorSelection = null;

    private PlayerController plCntrl;
    private Inventory inventory; // todo wrap this into inventoryToolUser where if top item is a tool, right click places it like a regular item
    // but any other interaction goes through the wrapper (so this is where plate code goes)
    public Inventory Inventory { get => inventory; }

    private void Awake()
    {
        plCntrl = GetComponent<PlayerController>();
        inventory = GetComponent<Inventory>();
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
        cursorSelection = obj;
    }

    private void UnTarget(Collider other)
    {
        if (!other.TryGetComponent(out IsInteractable obj)) return;
        obj.ToggleHighlight(false);
        cursorSelection = null;
    }
}
