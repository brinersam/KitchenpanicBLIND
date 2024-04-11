using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("Scene Transitions")]
    [SerializeField] Button LoadGameplaySceneBTN = null;
    [SerializeField] Button LoadMainMenuSceneBTN = null;


    [Header("Change menu")]
    [SerializeField] Button SettingsBTN = null;
    [SerializeField] MonoBehaviour IButton_SettingsBTNscript = null;


    [Header("Gameplay related")]
    [SerializeField] Button UnpauseBTN = null;


    [Header("System")]
    [SerializeField] Button QuitBTN = null;

    private void Start() 
    {
        ConfigureButton(LoadGameplaySceneBTN, ()=>Loader.LoadScene(Loader.ScenesEnum.Gameplay));
        ConfigureButton(LoadMainMenuSceneBTN, ()=>Loader.LoadScene(Loader.ScenesEnum.MainMenu));
        
        ConfigureButton(SettingsBTN, (IButton_SettingsBTNscript as IButton).OnClick);

        ConfigureButton(UnpauseBTN, ()=>System_Pauser.Pause_Off());

        ConfigureButton(QuitBTN, ()=>{Debug.Log("QUITTING...");Application.Quit();});
    }
    
    private void ConfigureButton(Button btn, UnityAction fun = null)
    {
        if (btn == null) return;
        
        btn.onClick.AddListener(fun);
    }
}
