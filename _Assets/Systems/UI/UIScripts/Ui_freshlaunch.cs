using System;
using UnityEngine;
public class UI_freshlaunch : Abstract_UI
{
    [SerializeField] UI_Element_Countdown _countdown;
    [SerializeField] TutorialContainer _instructionUI;
    private Action StartDelegate;

    public override void Activate(Abstract_UI prevstate = null)
    {
        gameObject.SetActive(true);
        System_Pauser.Pause_On();
        StartDelegate = CountDownToStart;
    }
    public override void OnMenuBtn()
    {
        StartDelegate?.Invoke();
    }

    private void CountDownToStart()
    {
        StartDelegate = null;
        _instructionUI.gameObject.SetActive(false);
        _countdown.StartSequence(3, () => System_Pauser.Pause_Off());
    }

    public override void OnUnPause()
    {
        SetStateTo(System_UI.state_gameplay);
    }
}