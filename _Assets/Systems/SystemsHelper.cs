using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemsHelper : MonoBehaviour
{
    [Header("Game Scenario")]
    [SerializeField] private GameDifficultyScenarioSO initialScenario;

    [Header("Dish request system")]
    const int MAX_RECIPES_IN_QUEUE = 5;

    [Header("Ui system")]
    public UI_freshlaunch UI_freshLaunchObj;
    public UI_gameplay UI_gameplayObj;
    public UI_pausemenu UI_pauseMenuObj;
    public UI_gameover UI_gameoverObj;

    [Header("Tick system")]
    [SerializeField] float triggerEventAtEachnthTick = 4f;
    private float curTick = 0;
    private float eventTick = 0;

    void Awake()
    {
        System_DishMgr.Helper = this;
        System_UI.Helper = this;
    }

    void Start() 
    {
        if (initialScenario == null)
            Debug.LogError("No scenario set! No recipes will be generated", this);
        else
            System_DishMgr.Scenario = initialScenario;
    }

    void FixedUpdate() 
    {
        curTick += 1 * Time.deltaTime;
        if (curTick >= 1) // 1 tick per second
        {
            System_DishMgr.OnTick();
            curTick -= 1;
            eventTick += 1;
        }

        if (eventTick >= triggerEventAtEachnthTick)
        {
            System_DishMgr.OnTickEvent();
            eventTick -= triggerEventAtEachnthTick;
        }
    }
}
