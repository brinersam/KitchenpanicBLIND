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
            if (!callerInv.IsFull || callerInv[0].prepareMethod != preparerType) return;

            inventory.InteractWInv(callerInv);
            TryStartMinigame();
            return;
        }
        
        inventory.InteractWInv(callerInv);
    }

    public void OnMinigameFinished()
    {
        if (!inventory.IsFull || !inventory[0].TryPrepare(out IHoldable result)) return;
        inventory[0] = result;
    }

    private void TryStartMinigame()
    {
        if (minigame == null)  // no minigame = prepare ingredient instantly
        {
            OnMinigameFinished();
            return;
        }
        minigame.StartMinigame();
    }
    
    private bool IsMinigameInProgress()
    {
        return minigame != null && minigame.CurState != MinigameBase.MinigameStateEnum.Offline;
    }
}
