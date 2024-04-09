using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class System_UI
{
    private static SystemsHelper __helper;
    public static SystemsHelper Helper 
    {   get => __helper;
        set
        {if (__helper != null)
            {   Debug.LogError("Interrupted attempt at second helper connecting to static @DishDeliverySystem",value.gameObject);
                return;
            }
            __helper = value;

            if (state == null)
                InitializeStates();
    }   }

    public static UI_freshlaunch state_freshLaunch; // this doesnt seem right
    public static UI_gameplay state_gameplay;
    public static UI_pausemenu state_pauseMenu;
    public static UI_gameover state_gameover;
    public static Abstract_UI state;

    static System_UI()
    {
        System_Pauser.OnPause += OnPause;
        System_Pauser.OnUnPause += OnUnPause;
        System_Tick.OnTick += OnTick;
    }

    public static void OnMenuBtn()
    {
        state.OnMenuBtn();
    }

    private static void OnPause()
    {
        state.OnPause();
    }
    
    private static void OnUnPause()
    {
        state.OnUnPause();
    }

    private static void OnTick()
    {
        state.OnTick();
    }

    private static void InitializeStates()
    {
        state_freshLaunch = Helper.UI_freshLaunchObj;
        state_gameplay = Helper.UI_gameplayObj;
        state_pauseMenu = Helper.UI_pauseMenuObj;
        state_gameover = Helper.UI_gameoverObj;
        state = state_freshLaunch;
        state.Activate();
    }
}
