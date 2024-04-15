using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

        // BindControlGroup(
        //     System_InputSystemLocator.InputSystem.Gameplay.Movement.bindings.GetEnumerator(),
        //     _buttonsSet[ControlGroupsEnum.Movement]);
        
        InSysTools.BindControlGroup(
            System_InputSystemLocator.InputSystem.Gameplay.AltInteract.bindings.GetEnumerator(),
            _buttonsSet[ControlGroupsEnum.Alt]);
        
        InSysTools.BindControlGroup(
            System_InputSystemLocator.InputSystem.Gameplay.MainInteract.bindings.GetEnumerator(),
            _buttonsSet[ControlGroupsEnum.Interact]);
        
        InSysTools.BindControlGroup(
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

    // private void BindControlGroup(IEnumerator<InputBinding> enum_i_InputBinding, List<BindingButton> buttonList)
    // {
    //     IEnumerator<BindingButton> enum_j_Buttons = buttonList.GetEnumerator();

    //     while (enum_i_InputBinding.MoveNext() && enum_j_Buttons.MoveNext())
    //     {
    //         InputBinding i = enum_i_InputBinding.Current;
    //         BindingButton j = enum_j_Buttons.Current;
    //         j.Initialize(i);
    //     }
    // }



    // private void DEBUG()
    // {
    //     foreach (InputBinding i in System_InputSystemLocator.InputSystem.bindings)
    //     {
            
    //         if (System_InputSystemLocator.InputSystem.FindBinding(i, out InputAction b) != 0) 
    //             continue;
                
    //         Debug.Log("\\/=================\\/");
    //         Debug.Log($"InputAction.type : {b.type}");
    //         Debug.Log($"InputAction.controls : \\/\\/\\/\\/\\/");
    //         Debug.Log("====\\/====");
    //         foreach(var j in b.controls)
    //         {
    //             Debug.Log($"InputControl.name : {j.name}");
                    
    //             Debug.Log($"InputControl.displayName : {j.displayName}");
    //             Debug.Log($"InputControl.path : {j.path}");
    //         }
    //         Debug.Log("===/\\=====");
    //         Debug.Log($"InputAction.bindings : \\/\\/\\/\\/\\/");
    //         foreach(var j in b.bindings)
    //         {
    //             Debug.Log($"InputBinding.name : {j.name}");
    //             Debug.Log($"InputBinding.id : {j.id}");
    //             Debug.Log($"InputBinding.path : {j.path}");
    //             Debug.Log($"InputBinding.isPartOfComposite : {j.isPartOfComposite}");
    //         }
    //         Debug.Log($"InputAction.bindingMask : {b.bindingMask}");
    //         Debug.Log($"InputAction.GetBindingDisplayString() : {b.GetBindingDisplayString()}");
    //         Debug.Log("/\\=================/\\");
    //     }
    // }
}
