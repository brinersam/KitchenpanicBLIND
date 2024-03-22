using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    private enum UIStateenum 
    {
        freshLaunch = 0,
        gameplay = 1,
        pauseMenu = 2,
    }
    [SerializeField] GameObject UI_freshLaunch;
    [SerializeField] GameObject UI_gameplay;
    [SerializeField] GameObject UI_pauseMenu;

    private UIStateenum __stateUi; // access only through property 
    private UIStateenum StateUI
    {
        get{return __stateUi;}
        set{SetStateTo(value);}
    }

    private void Awake()
    {
        PauseManager.OnPause += () => StateUI = UIStateenum.pauseMenu;
        PauseManager.OnUnPause += () => StateUI = UIStateenum.gameplay;
        StateUI = UIStateenum.freshLaunch;
    }

    public void OnMenuBtn()
    {
        switch (StateUI)
        {
            case UIStateenum.freshLaunch:
            {
                PauseManager.Pause_Off();
                break;
            }
            case UIStateenum.gameplay:
            {
                PauseManager.Pause_On();
                break;
            }
            case UIStateenum.pauseMenu:
            {
                PauseManager.Pause_Off();
                break;
            }
        }
    }

    private void SetStateTo(UIStateenum newState) // todo refactor to a proper state pattern
    {
        DisableCurrentUi();
        switch (newState)
        {
            case UIStateenum.freshLaunch:
            {
                UI_freshLaunch.SetActive(true);
                break;
            }
            case UIStateenum.gameplay:
            {
                UI_gameplay.SetActive(true);
                break;
            }
            case UIStateenum.pauseMenu:
            {
                UI_pauseMenu.SetActive(true);
                break;
            }
        }
        __stateUi = newState;
    }

    private void DisableCurrentUi()
    {
        switch (StateUI)
        {
            case UIStateenum.freshLaunch:
            {
                UI_freshLaunch.SetActive(false);
                break;
            }
            case UIStateenum.gameplay:
            {
                UI_gameplay.SetActive(false);
                break;
            }
            case UIStateenum.pauseMenu:
            {
                UI_pauseMenu.SetActive(false);
                break;
            }
        }
    }
}
