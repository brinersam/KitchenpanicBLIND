using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button StartBTN;
    [SerializeField] Button SettingsBTN;
    [SerializeField] Button QuitBTN;

    private void Start() 
    {
        StartBTN.onClick.AddListener(()=>Loader.LoadScene(Loader.ScenesEnum.Gameplay));
        SettingsBTN.onClick.AddListener(bruh);
        QuitBTN.onClick.AddListener(()=>{Debug.Log("QUITTING...");Application.Quit();});
    }
    
    public void bruh()
    {
        Debug.Log("HIIIIIII");
    }
}
