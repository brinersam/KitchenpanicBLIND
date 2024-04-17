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

        if (topmostItem.Info.Type == ItemType.Container)
        {
            if (System_DishMgr.TryAcceptDish(topmostItem))
            {
                System_Audio.Instance.PlaySoundOfType(SoundType.Delivery_Success);
                playerInv.Item_Lose();
            }
            else
            {
                System_Audio.Instance.PlaySoundOfType(SoundType.Delivery_Failure);
            }
                
        }
    }

}
