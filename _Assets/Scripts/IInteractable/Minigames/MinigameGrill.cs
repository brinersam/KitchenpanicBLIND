using System.Collections;
using UnityEngine;

public class MinigameGrill : MinigameBase
{
    [SerializeField] private int heatPower = 4;
    private int heatRequired = 0;
    private int heatLeft;

    public override void StartMinigame()
    {
        IHoldable item = ((IsItemPreparer)caller).ContainedItem;

        if (item.prepareResult == null) // todo make it use inventory system instead
            return;

        heatRequired = item.ChopsOrHeatsRequired;
        heatLeft = heatRequired;

        StartCoroutine("Minigame");
        CurState = MinigameStateEnum.Ongoing;
    }
    public override void InterruptMinigame()
    {
        StopCoroutine("Minigame");
        CurState = MinigameStateEnum.Offline;
    }
    
    protected override void Interact_Ongoing(bool alt)
    {
        InterruptMinigame();
    }

    private IEnumerator Minigame()
    {
        while (heatLeft > 0)
        {
            heatLeft -= heatPower;
            progressBar.Refresh(1 - (float)heatLeft/heatRequired);
            yield return new WaitForSeconds(1); 
        }
        FinishMinigame();
        StartMinigame();
        yield break;
    }
}
