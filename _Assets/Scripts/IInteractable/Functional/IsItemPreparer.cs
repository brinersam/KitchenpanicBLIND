using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsInteractable))]
[RequireComponent(typeof(Inventory))]
public class IsItemPreparer : MonoBehaviour, IInteractable, IMinigameSubscriber, IInventory
{
    [SerializeField] private PreparationEnum preparerType;
    [SerializeField] private MinigameBase minigame;
    [SerializeField] private Inventory inventory;
    public Inventory Inventory => inventory;

    void Awake()
    {
        minigame.Initialize(this);
    }

    public void Interact(MonoBehaviour caller, bool alt)
    {
        if (IsMinigameInProgress()) // redirect control to minigame
        {
            minigame.Interact(alt);
            return;
        }

        Inventory callerInv = (caller as IInventory).Inventory;

        if (!inventory.IsFull) 
        {
            if (callerInv.Item_Peek() != null && callerInv.Item_Peek().prepareMethod != preparerType) return; // this is ass, make generic whitelist instead todo

            inventory.Item_TryReceive(callerInv);
            minigame.StartMinigame();
            return;
        }
        
        inventory.Pull_PushItem(callerInv);
    }

    public void OnMinigameFinished()
    {
        if (!inventory.IsFull || !inventory.Item_Peek().TryPrepare(out Holdable result)) return;
        inventory.Item_Replace(result);
    }
    
    private bool IsMinigameInProgress()
    {
        return minigame != null && minigame.CurState != MinigameBase.MinigameStateEnum.Offline;
    }
}
