using UnityEngine;
public class UI_gameover : Abstract_UI
{
    public override void Activate()
    {
        uiGobj.SetActive(true);
        System_Pauser.Pause_On();
    }

}