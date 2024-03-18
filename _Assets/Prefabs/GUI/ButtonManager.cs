using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] Button StartBTN = null;
    [SerializeField] Button SettingsBTN = null;
    [SerializeField] Button QuitBTN = null;

    private void Start() 
    {
        ConfigureButton(StartBTN, ()=>Loader.LoadScene(Loader.ScenesEnum.Gameplay));
        ConfigureButton(SettingsBTN);
        ConfigureButton(QuitBTN, ()=>{Debug.Log("QUITTING...");Application.Quit();});
    }
    
    private void ConfigureButton(Button btn, UnityAction fun = null)
    {
        if (btn == null) return;
        
        btn.onClick.AddListener(fun);
    }

}
