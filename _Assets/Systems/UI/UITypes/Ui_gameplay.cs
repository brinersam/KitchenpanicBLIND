using UnityEngine;
public class UI_gameplay : Abstract_UI
{
    [SerializeField] private DishMgr_UI UI;

    public override void OnMenuBtn()
    {
        System_Pauser.Pause_On();
    }

    // public override void RefreshQueue()
    // {
    //     UI.UpdateVisuals(System_DishMgr.RecipeQueue);
    // }

    public override void OnPause()
    {
        SetStateTo(System_UI.state_pauseMenu);
    }

}