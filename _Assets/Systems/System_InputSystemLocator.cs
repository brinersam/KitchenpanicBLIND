using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class System_InputSystemLocator
{
    public readonly static InputSystem InputSystem;
    public static InputActionAsset InputActionAsset {get;set;}

    private static SystemsHelper _helper = null;
    private static int _timeCur;
    public static SystemsHelper Helper 
    {   get => _helper;
        set {Initialze(value);}
    }

    static System_InputSystemLocator()
    {
        InputSystem = new InputSystem();
    }

    private static void Initialze(SystemsHelper val)
    {
        if (_helper != null)
        {   Debug.LogError("Interrupted attempt at second helper connecting to static @System_DishManager",val.gameObject);
            return;
        }
        _helper = val;
        InputActionAsset = _helper.InputActionAsset;
    }
}
