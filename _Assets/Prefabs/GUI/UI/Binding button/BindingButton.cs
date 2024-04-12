using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class BindingButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _bindingText;
    private InputBinding _inputAction;
    public TMP_Text BindingText => _bindingText;
    
    public void Initialize(InputBinding IB)
    {
        _inputAction =  IB;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        _bindingText.text = _inputAction.name;
    }

    private void OnMouseDown() 
    {
        Debug.Log($"{_bindingText.text} been clicked!");
    }
}
