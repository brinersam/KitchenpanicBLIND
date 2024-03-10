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

        if (!playerInv.IsEmpty && playerInv.Item_Peek().Info.type == ItemType.Container)
        {   
            //callerInv.Item_Peek().Inventory.Item_TryReceiveFrom(this.inventory);
            if (alt)
            {
                inventory.Item_TryReceiveFrom(playerInv); // todo
                Debug.Log($"{this.name} was updated!", gameObject);
                foreach (var item in inventory)
                {
                    Debug.Log($"I have {item.Info.name}, {item.Info.name}.Inventory empty? {item.Inventory.IsEmpty}"); // item.Inventory returns null even if it is initialized... great
                    if (!item.Inventory.IsEmpty)
                    {
                        Debug.Log($"My {item.Info.name} has: ");
                        foreach (var subitem in item.Inventory)
                        {
                            Debug.Log($"{subitem.Info.name}, ");
                        }
                    }
                }
            }
                
            else
            {
                inventory.ItemPullPushFrom(playerInv.Item_Peek().Inventory);
            }
            return;
        }
        inventory.ItemPullPushFrom(playerInv);
    }
}
