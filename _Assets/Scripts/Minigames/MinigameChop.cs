using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameClick : MinigameBase // change this to minigame_clicker for modularity
{
    [SerializeField] AudioClip[] chopSounds;

    [SerializeField] private int clickPower = 1;
    private int clicksRequired = 0;
    private int clicksLeft;

    public override void Minigame_Start()
    {
        var item = (caller as IInventory).Inventory.Item_Peek();
        
        clicksRequired = item.Info.ChopsOrHeatsRequired;
        clicksLeft = clicksRequired;
        CurState = MinigameStateEnum.Ongoing;
    }

    protected override void Interact_Ongoing(bool alt, out bool rc)
    {
        rc = false;
        if (alt)
        {
            Minigame_Interrupt();
        }

        audioSrc.PlayOneShot(chopSounds[Random.Range(0,chopSounds.Length)]);
        clicksLeft -= clickPower;
        progressBar.Refresh(1- (float)clicksLeft/clicksRequired);
        if (clicksLeft <= 0)
        {
            Minigame_ForceFinish();
        }
    }
}
