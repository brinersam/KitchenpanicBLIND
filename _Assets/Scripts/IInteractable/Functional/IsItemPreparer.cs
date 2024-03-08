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
            Item item = callerInv.Item_Peek();
            if (item == null || item.Info.prepareMethod != preparerType) return;
            inventory.Item_TryReceiveFrom(callerInv);

            minigame.StartMinigame();
            return;
        }
        
        callerInv.Item_TryReceiveFrom(inventory);
    }

    public void OnMinigameFinished()
    {
        if (inventory.IsEmpty) return;

        if (inventory.Item_Peek().TryReturnPreparedVersion(out Item result))
            inventory.Item_Replace(result);

        // inventory.Item_Peek().BecomePrepared();
    }
    
    private bool IsMinigameInProgress()
    {
        return minigame != null && minigame.CurState != MinigameBase.MinigameStateEnum.Offline;
    }
}
