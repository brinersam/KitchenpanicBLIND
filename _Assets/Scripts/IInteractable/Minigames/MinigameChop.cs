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
        var item = (caller as IInventory).Inventory.Item_Peek();
        
        chopsRequired = item.Info.ChopsOrHeatsRequired;
        chopsLeft = chopsRequired;
        CurState = MinigameStateEnum.Ongoing;
    }

    protected override void Interact_Ongoing(bool alt)
    {
        if (alt)
        {
            InterruptMinigame();
        }

        chopsLeft -= chopPower;
        progressBar.Refresh(1- (float)chopsLeft/chopsRequired);
        if (chopsLeft <= 0)
        {
            FinishMinigame();
        }
    }
}
