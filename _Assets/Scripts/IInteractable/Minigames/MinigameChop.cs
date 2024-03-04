using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameChop : MinigameBase
{
    [SerializeField] private int chopPower = 4;
    private int chopsRequired = 0;
    private int chopsLeft;

    public override void StartMinigame()
    {
        IHoldable item = ((IsItemPreparer)caller).ContainedItem;
        if (item.prepareResult == null) // todo make it use inventory system instead
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
