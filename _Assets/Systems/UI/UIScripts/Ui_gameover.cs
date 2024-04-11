using UnityEngine;
public class UI_gameover : Abstract_UI
{
    public override void Activate(Abstract_UI prevstate = null)
    {
        gameObject.SetActive(true);
        System_Pauser.Pause_On();
    }

}