using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDeliveryCounter : MonoBehaviour, IInteractable
{

    public void Interact(MonoBehaviour caller, bool alt)
    {
        Inventory playerInv = (caller as IInventory).Inventory;
        if (playerInv.IsEmpty) return;

        Item topmostItem = playerInv.Item_Peek();

        if (topmostItem.Info.type == ItemType.Container)
        {
            if (DishDeliverySystem.TryAcceptDish(topmostItem))
                playerInv.Item_Lose();
        }
    }

}
