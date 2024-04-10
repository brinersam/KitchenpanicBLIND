using UnityEngine;
public class UI_gameplay : Abstract_UI
{
    [SerializeField] private DishMgr_UI _UIScript;
    public DishMgr_UI UIScript => _UIScript;
    
    public override void OnMenuBtn()
    {
        System_Pauser.Pause_On();
    }

    public override void OnPause()
    {
        SetStateTo(System_UI.state_pauseMenu);
    }

    public override void OnGameOver()
    {
        SetStateTo(System_UI.state_gameover);
    }

}