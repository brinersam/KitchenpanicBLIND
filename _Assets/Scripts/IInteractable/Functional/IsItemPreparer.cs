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

        if (inventory.IsEmpty)
        {
            if (callerInv[0].prepareMethod != preparerType) return;

            inventory.TryReceiveItem(callerInv);
            TryStartMinigame();
        }
        else
        {
            callerInv.TryReceiveItem(inventory);
            return;
        }
    }

    public void OnMinigameFinished()
    {
        if (inventory.IsEmpty || !inventory[0].TryPrepare(out IHoldable result)) return;
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
