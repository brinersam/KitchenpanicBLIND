using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsInteractable))]
public class IsItemGiver : MonoBehaviour, IInteractable
{
    [SerializeField]
    Holdable itemToGive;
    [SerializeField]
    SpriteRenderer applyIconTo;

    private void Start()
    {
        if (itemToGive == null)
        {
            Debug.LogError("Itemgiver was not set!!", transform);
            return;
        }
        
        if (applyIconTo != null)
            applyIconTo.sprite = itemToGive.icon;
    }
    public void Interact(MonoBehaviour caller, bool alt)
    {
        Inventory callerInv = (caller as IInventory).Inventory;

        callerInv.Item_TryReceive(Instantiate(itemToGive));
    }
}
