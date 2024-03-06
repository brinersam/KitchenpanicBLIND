using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Inventory))]
public class PlayerCursor : MonoBehaviour, IInventory
{
    public IInteractable cursorSelection = null;
    
    [SerializeField] private HasItemDisplayer itemDisplayer;
    [SerializeField] private PlayerController plCntrl;
    [SerializeField] private Inventory inventory;
    public Inventory Inventory { get => inventory; }

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
}
