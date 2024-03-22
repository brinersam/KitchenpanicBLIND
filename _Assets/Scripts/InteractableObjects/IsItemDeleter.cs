using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsInteractable))]
public class IsItemDisposer : MonoBehaviour, IInteractable
{
    public void Interact(MonoBehaviour caller, bool alt)
    {
        Inventory callerInv = (caller as IInventory).Inventory;
        callerInv.Item_Lose();
    }
}
