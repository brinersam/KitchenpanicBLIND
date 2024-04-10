using System;
using UnityEngine;

public static class System_Tick
{
    [SerializeField] static float triggerEventAtEachnthTick = 4f;
    private static float curTick = 0;
    private static float eventTick = 0;

    public static event Action OnTick;
    public static event Action OnTickEvent;

    private static SystemsHelper _helper = null;
    public static SystemsHelper Helper 
    {   
        get => _helper;
        set
        {
            if (_helper != null)
            {   Debug.LogError("Interrupted attempt at second helper connecting to static @System_Tick",value.gameObject);
                return;
            }
            _helper = value;
        }
    }

    public static void FixedUpdate() 
    {
        curTick += 1 * Time.deltaTime;
        
        if (curTick >= 1) // 1 tick per second
        {
            OnTick?.Invoke();
            curTick -= 1;
            eventTick += 1;
        }

        if (eventTick >= triggerEventAtEachnthTick)
        {
            OnTickEvent?.Invoke();
            eventTick -= triggerEventAtEachnthTick;
        }
    }
}
