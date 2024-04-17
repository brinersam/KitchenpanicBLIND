using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class System_InputSystemLocator
{
    public static InputSystem InputSystem {get; private set;}
    public static InputActionAsset InputActionAsset {get;private set;}

    private static SystemsHelper _helper = null;
    public static SystemsHelper Helper 
    {   get => _helper;
        set {Initialze(value);}
    }

    private static void Initialze(SystemsHelper val)
    {
        if (_helper != null)
        {   Debug.LogError("Interrupted attempt at second helper connecting to static @System_InputSystemLocator",val.gameObject);
            return;
        }
        _helper = val;
        InputActionAsset = _helper.InputActionAsset;
        InputSystem = new InputSystem();
    }
}
