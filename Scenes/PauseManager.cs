using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{
    private static bool pauseActive = false;
    public static bool PauseActive => pauseActive;
    
    public static event Action OnPause;
    public static event Action OnUnPause;

    public static void Pause_Toggle()
    {
        if (pauseActive)
            Pause_Off();
        else
            Pause_On();
    }

    public static void Pause_On()
    {
        pauseActive = true;
        Time.timeScale = 0;
        OnPause?.Invoke();
    }

    public static void Pause_Off()
    {
        pauseActive = false;
        Time.timeScale = 1;
        OnUnPause?.Invoke();
    }

}
