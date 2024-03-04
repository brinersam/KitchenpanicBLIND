using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(IsInteractable))]
//[RequireComponent(typeof(IsItemGiver))]

public class IsItemPreparer : MonoBehaviour, IInteractable, IMinigameSubscriber
{
    // [SerializeField] // really all inventory functions should go through an interface todo
    // private IInventory inventory;
    
    [SerializeField] private PreparationEnum preparerType;
    [SerializeField] private ItemDisplayer displayer;
    [SerializeField] private MinigameBase minigame;

    private IHoldable containedItem;
    public IHoldable ContainedItem
    {
        get { return containedItem; }
        private set
        {
            containedItem = value;
            displayer.UpdateVisuals(containedItem);
        }
    }

    private void Awake()
    {
        minigame.Initialize(this);
    }

    public void Interact(PlayerCursor caller, bool alt)
    {
        if (IsMinigameInProgress()) // redirect control to minigame
        {
            minigame.Interact(alt);
            return;
        }
        
        if ((ContainedItem == null) == (caller.Inventory == null)) return; // xnor we cant have both (empty hands and empty grill) and (busy hands and busy grill)
        
        if (caller.Inventory == null)
        {
            caller.ReceiveItem(ContainedItem);
            ContainedItem = null;
            return;
        }

        if (ContainedItem == null)
        {
            if (caller.Inventory.prepareMethod != preparerType) return;

            ContainedItem = caller.LoseItem();

            if (minigame == null)
            {
                OnMinigameFinished();
                return;
            }

            minigame.StartMinigame();
            return;
        }
    }
    private bool IsMinigameInProgress()
    {
        return minigame != null && minigame.CurState != MinigameBase.MinigameStateEnum.Offline;
    }
    public void OnMinigameFinished()
    {
        if (ContainedItem == null || !ContainedItem.TryPrepare(out IHoldable result)) return;
        ContainedItem = result;
    }
}
