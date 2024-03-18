using System.Collections;
using UnityEngine;

public class MinigameGrill : MinigameBase // change this to minigame_wait for modularity
{
    [SerializeField] private int heatPower = 4;
    private int heatRequired = 0;
    private int heatLeft;

    public override void Minigame_Start()
    {
        Item item = (caller as IInventory).Inventory.Item_Peek();
        if (item.Info.prepareResult == null) return;

        heatRequired = item.Info.ChopsOrHeatsRequired;
        heatLeft = heatRequired;

        StartCoroutine("Minigame");
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

    private IEnumerator Minigame()
    {
        while (heatLeft > 0)
        {
            heatLeft -= heatPower;
            progressBar.Refresh(1 - (float)heatLeft/heatRequired);
            yield return new WaitForSeconds(1); 
        }
        Minigame_ForceFinish();
        Minigame_Start();
        yield break;
    }
}
