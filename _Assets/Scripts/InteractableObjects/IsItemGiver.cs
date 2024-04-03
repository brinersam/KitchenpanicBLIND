using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsInteractable))]
public class IsItemGiver : MonoBehaviour, IInteractable
{
    [SerializeField]
    ItemInfo itemToGive;
    [SerializeField]
    SpriteRenderer applyIconTo;

    private void Awake()
    {
        if (itemToGive == null)
        {
            Debug.LogError("Itemgiver was not set!!", transform);
            return;
        }
        
        if (applyIconTo != null)
            applyIconTo.sprite = itemToGive.Sprite;
    }
    public void Interact(MonoBehaviour caller, bool alt)
    {
        Inventory callerInv = (caller as IInventory).Inventory;

        callerInv.Item_TryReceive(itemToGive.SpawnItem());
    }
}
