using System.Collections;
using UnityEngine;

public class MinigameWait : MinigameBase // change this to minigame_wait for modularity
{
    [SerializeField] private float secPower = 1;
    private int secsRequired = 0;
    private float secsLeft;

    Coroutine minigame;


    public override void Minigame_Start()
    {
        Item item = (caller as IInventory).Inventory.Item_Peek();
        if (item.Info.prepareResult == null) return;

        secsRequired = item.Info.ChopsOrHeatsRequired;
        secsLeft = secsRequired;

        StartCoroutine("Minigame",item);
        CurState = MinigameStateEnum.Ongoing;
    }
    public override void Minigame_Interrupt()
    {
        StopCoroutine("Minigame");
        CurState = MinigameStateEnum.Offline;
    }
    
    protected override void Interact_Ongoing(bool alt, out bool rc)
    {
        rc = true;
        Minigame_Interrupt();
    }

    private IEnumerator Minigame(Item item)
    {
        while (secsLeft > 0)
        {
            secsLeft -= secPower/2;
            float pct = 1 - secsLeft/secsRequired;
            progressBar.Refresh(pct);

            if (item.Info.prepareResult.type == ItemType.Ingredient_Trash)
            {
                if (pct > 0.6)
                    progressBar.Warning_Pulse();
                if (pct > 0.9)
                    progressBar.Warning_Pulse(true);
            }

            yield return new WaitForSeconds(0.5f); 
        }
        Minigame_ForceFinish();
        Minigame_Start();
        yield break;
    }
}
