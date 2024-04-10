using UnityEngine;
public class UI_freshlaunch : Abstract_UI
{
    public override void Activate()
    {
        gameObject.SetActive(true);
        System_Pauser.Pause_On();
    }
    public override void OnMenuBtn()
    {
        System_Pauser.Pause_Off();
    }

    public override void OnUnPause()
    {
        SetStateTo(System_UI.state_gameplay);
    }
}