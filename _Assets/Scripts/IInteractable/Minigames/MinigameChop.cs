using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameChop : MinigameBase // change this to minigame_clicker for modularity
{
    [SerializeField] private int chopPower = 4;
    private int chopsRequired = 0;
    private int chopsLeft;

    public override void StartMinigame()
    {
        Holdable item = (caller as IInventory).Inventory.Item_Peek();
        if (item == null || item.prepareResult == null)
            return;
        
        chopsRequired = item.ChopsOrHeatsRequired;
        chopsLeft = chopsRequired;
        CurState = MinigameStateEnum.Ongoing;
    }

    protected override void Interact_Ongoing(bool alt)
    {
        if (alt)
        {
            InterruptMinigame();
        }

        chopsLeft -= 1;
        progressBar.Refresh(1- (float)chopsLeft/chopsRequired);
        if (chopsLeft <= 0)
        {
            FinishMinigame();
        }
    }
}
