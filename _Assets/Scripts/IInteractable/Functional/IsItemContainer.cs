using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(IsInteractable))]
public class IsItemContainer : MonoBehaviour, IInteractable, IInventory
{
    [SerializeField] private Inventory inventory;
    public Inventory Inventory => inventory;

    public void Interact(MonoBehaviour caller, bool alt)
    {
        Inventory playerInv = (caller as IInventory).Inventory;
        Item playerTopSlot = playerInv.Item_Peek();

        if (playerTopSlot != null && playerTopSlot.Info.type == ItemType.Container)
        {   
            if (alt || playerTopSlot.Inventory.IsEmpty)
            {
                inventory.Item_TryReceiveFrom(playerInv);
            }
            else
            {
                inventory.Item_PullPushFrom(playerTopSlot.Inventory); // plate pickup dont work fix this todo
                playerInv.UpdateVisuals();
            }
            return;
        }
        
        if (inventory.IsEmpty == false && playerInv.IsEmpty == false && inventory.Item_Peek().Info.type == ItemType.Container)
        {
            if (inventory.Item_Peek().Inventory.Item_TryReceiveFrom(playerInv))
                inventory.UpdateVisuals();
            return;
        }

        inventory.Item_PullPushFrom(playerInv);
    }
}
