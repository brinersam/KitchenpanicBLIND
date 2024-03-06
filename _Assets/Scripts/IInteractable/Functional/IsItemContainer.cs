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
        Inventory callerInv = (caller as IInventory).Inventory;

        inventory.Pull_PushItem(callerInv);
    }
}
