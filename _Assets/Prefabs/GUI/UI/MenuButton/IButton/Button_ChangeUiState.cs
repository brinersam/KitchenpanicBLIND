using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ChangeUiState : MonoBehaviour, IButton
{
    public void OnClick()
    {
        System_UI.state.SetStateTo(System_UI.state_settings);
    }
}
