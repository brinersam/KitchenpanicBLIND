using System;
using UnityEngine;
public static class System_Session
{
    private static GameDifficultyScenarioSO _scenario;
    private static SystemsHelper _helper = null;
    private static int _timeCur;
    public static SystemsHelper Helper 
    {   get => _helper;
        set {Initialze(value);}
    }

    public static int TimeCur 
    {   get => _timeCur;
        set{TimeMax = Math.Max(TimeMax,value);
            _timeCur = value;}
    }

    public static GameDifficultyScenarioSO Scenario 
    {   get => _scenario;
        set{_scenario = value;
            OnScenarioSet();}
    }

    public static int TimeMax {get;private set;} = 0;
    public static event Action OnGameOver;

    private static void Initialze(SystemsHelper val)
    {
        if (_helper != null)
        {   Debug.LogError("Interrupted attempt at second helper connecting to static @System_DishManager",val.gameObject);
            return;
        }
        _helper = val;
        System_Tick.OnTick += OnTick;
    }

    private static void OnScenarioSet()
    {
        TimeCur = Scenario.initialSeconds;
    }

    private static void OnTick()
    {
        TimeCur--;
        if (TimeCur <= 0)
            OnGameOver?.Invoke();
    }
}
