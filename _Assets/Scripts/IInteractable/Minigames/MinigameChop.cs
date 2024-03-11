using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameChop : MinigameBase // change this to minigame_clicker for modularity
{
    [SerializeField] private int chopPower = 4;
    private int chopsRequired = 0;
    private int chopsLeft;

    public override void Minigame_Start()
    {
        var item = (caller as IInventory).Inventory.Item_Peek();
        
        chopsRequired = item.Info.ChopsOrHeatsRequired;
        chopsLeft = chopsRequired;
        CurState = MinigameStateEnum.Ongoing;
    }

    protected override void Interact_Ongoing(bool alt, out bool rc)
    {
        rc = false;
        if (alt)
        {
            Minigame_Interrupt();
        }

        chopsLeft -= chopPower;
        progressBar.Refresh(1- (float)chopsLeft/chopsRequired);
        if (chopsLeft <= 0)
        {
            Minigame_ForceFinish();
        }
    }
}
