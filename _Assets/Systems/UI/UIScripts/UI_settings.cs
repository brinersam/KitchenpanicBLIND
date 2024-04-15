using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Custom.InputSystemTools;

public class UI_settings : Abstract_UI
{
    [SerializeField] private Transform _buttonContainer;
    [SerializeField] private Button _backButton;
    [SerializeField] private Dictionary<ControlGroupsEnum,List<BindingButton>> _buttonsSet = new();
    [SerializeField] private InputSystem _inputSystem;

    private bool _isInitialized = false;
    private Abstract_UI _prevState;
    
    public override void Activate(Abstract_UI prevstate = null)
    {
        _prevState = prevstate;
        gameObject.SetActive(true);
        Initialize();
    }

    private void OnBackButton()
    {
        System_UI.state.SetStateTo(_prevState);
    }

    private void Initialize()
    {
        if (_isInitialized) return;
            _isInitialized = true;

        _backButton.onClick.AddListener(OnBackButton);
        FillButtonSetDict();

        // it is possible to automatically generate input settings depending on map/maps which is why this mess is here,
        // for that use same dict thing but for key use typeof InputSystem.GameplayActions.%actionname such as AltInteract%
        // im not motivated enough rn though

        // BindControlGroup(
        //     System_InputSystemLocator.InputSystem.Gameplay.Movement.bindings.GetEnumerator(),
        //     _buttonsSet[ControlGroupsEnum.Movement]);
        
        InputSystemTools.BindControlGroup(
            System_InputSystemLocator.InputSystem.Gameplay.AltInteract.bindings.GetEnumerator(),
            _buttonsSet[ControlGroupsEnum.Alt]);
        
        InputSystemTools.BindControlGroup(
            System_InputSystemLocator.InputSystem.Gameplay.MainInteract.bindings.GetEnumerator(),
            _buttonsSet[ControlGroupsEnum.Interact]);
        
        InputSystemTools.BindControlGroup(
            System_InputSystemLocator.InputSystem.Gameplay.MenuButton.bindings.GetEnumerator(),
            _buttonsSet[ControlGroupsEnum.Pause]);
    }

    private void FillButtonSetDict()
    {
        foreach (Transform child in _buttonContainer)
        {
            BindingContainer btn = child.GetComponent<BindingContainer>();
            _buttonsSet[btn.ControlGroup] = btn.Buttons;
        }
    }
}
