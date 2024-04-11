using UnityEngine;
public class UI_pausemenu : Abstract_UI
{
    public override void OnMenuBtn()
    {
        System_Pauser.Pause_Off();
    }

    public override void OnUnPause()
    {
        SetStateTo(System_UI.state_gameplay);
    }
}

