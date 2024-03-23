using UnityEngine;
public class UI_gameplay : Abstract_UI
{
    [SerializeField] InvGUI orderUi;

    public override void OnMenuBtn()
    {
        System_Pauser.Pause_On();
    }

    public override void RefreshQueue()
    {
        orderUi.UpdateVisuals(System_DishMgr.RecipeQueue);
    }

    public override void OnPause()
    {
        SetStateTo(System_UI.state_pauseMenu);
    }

}