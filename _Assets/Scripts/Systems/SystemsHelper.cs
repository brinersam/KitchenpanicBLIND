using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SystemsHelper : MonoBehaviour
{
    [Header("Game Difficulty")]
    [SerializeField] private GameDifficultyScenarioSO initialScenario;

    [Header("Dish request system")]
    public const int MAX_RECIPES_IN_QUEUE = 5;

    [Header("Ui system")]
    public UI_freshlaunch UI_freshLaunchObj;
    public UI_gameplay UI_gameplayObj;
    public UI_pausemenu UI_pauseMenuObj;
    public UI_gameover UI_gameoverObj;
    public UI_settings UI_settingsObj;

    [Header("Input system")]
    public InputActionAsset InputActionAsset;

    void Awake()
    {
        System_Tick.Helper = this;
        System_DishMgr.Helper = this;
        System_UI.Helper = this;
        System_Session.Helper = this;
        System_InputSystemLocator.Helper = this;
    }

    void Start() 
    {
        if (initialScenario == null)
            Debug.LogError("No scenario set! No recipes will be generated", this);
        else
            System_Session.Scenario = initialScenario;
    }

    void FixedUpdate()
    {
        System_Tick.FixedUpdate();
    }
}
