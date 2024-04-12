using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    private void OnButton()
    {
        System_UI.state.SetStateTo(_prevState);
    }

    private void Initialize()
    {
        if (_isInitialized) return;
        _isInitialized = true;

        _backButton.onClick.AddListener(OnButton);
        FillButtonSetDict();

        using IEnumerator<InputBinding> enum_i_InputBinding = System_InputSystemLocator.InputSystem.Gameplay.Movement.bindings.GetEnumerator();
        using IEnumerator<BindingButton> enum_j_Buttons = _buttonsSet[ControlGroupsEnum.Movement].GetEnumerator();

        enum_i_InputBinding.MoveNext(); // skips first glue "composite" bind straight to separte subcomposite binds

        while (enum_i_InputBinding.MoveNext() && enum_j_Buttons.MoveNext())
        {
            InputBinding i = enum_i_InputBinding.Current;
            BindingButton j = enum_j_Buttons.Current;
            j.Initialize(i);
            Debug.Log($"{i.name}",j.gameObject);
            //j.Initialize()
        }
    }

    private void FillButtonSetDict()
    {
        foreach (Transform child in _buttonContainer)
        {
            BindingContainer btn = child.GetComponent<BindingContainer>();
            _buttonsSet[btn.ControlGroup] = btn.Buttons;
        }
    }

    // ♪♪♪ I'm not a big fan of the input system ♪
    // ♪♪♪ I'm not a big fan of the input system (30 on 30) ♪
    // ♪♪♪ I'm not a big fan of the input system (30 on 30) ♪
    // ♪♪♪ I'm not a big fan of the input system (30 30 30 30 30 30) ♪
    private void DEBUG()
    {
        Action<InputControl> act = (x) => Debug.Log($"{x.path} :: {x.path}");

        //Debug.Log(System_InputSystemLocator.InputSystem.Gameplay.Get());
        //Debug.Log(System_InputSystemLocator.InputActionAsset.FindBinding());
        int abc = 0;
        foreach (InputBinding i in System_InputSystemLocator.InputSystem.bindings)
        {
            //Debug.Log(i.id);
            // foreach(var j in i.bindings)
            // {
            //     Debug.Log(j.name);
            //     Debug.Log(j.path);
            //     Debug.Log(System_InputSystemLocator.InputSystem.FindBinding(j.name));
            // }

            if ((i.isPartOfComposite || i.isComposite) && abc++ > 0) // need both checks because it lists composite first then all subbindings of said composite one
            {
                Debug.Log($"Composite binidng of {i.GetNameOfComposite()} was detected");
                continue;
            }   
            
            if (System_InputSystemLocator.InputSystem.FindBinding(i, out InputAction b) != 0) 
                continue;
                
            Debug.Log("\\/=================\\/");
            Debug.Log($"InputAction.type : {b.type}");
            Debug.Log($"InputAction.controls : \\/\\/\\/\\/\\/");
            Debug.Log("====\\/====");
            foreach(var j in b.controls)
            {
                Debug.Log($"InputControl.name : {j.name}");
                    
                Debug.Log($"InputControl.displayName : {j.displayName}");
                Debug.Log($"InputControl.path : {j.path}");
            }
            Debug.Log("===/\\=====");
            Debug.Log($"InputAction.bindings : \\/\\/\\/\\/\\/");
            foreach(var j in b.bindings)
            {
                Debug.Log($"InputBinding.name : {j.name}");
                Debug.Log($"InputBinding.id : {j.id}");
                Debug.Log($"InputBinding.path : {j.path}");
                Debug.Log($"InputBinding.isPartOfComposite : {j.isPartOfComposite}");
            }
            Debug.Log($"InputAction.bindingMask : {b.bindingMask}");
            Debug.Log($"InputAction.GetBindingDisplayString() : {b.GetBindingDisplayString()}");
            //Debug.Log(b.GetBindingForControl(System_InputSystemLocator.InputSystem.Gameplay.Movement.controls[0]));
            Debug.Log("/\\=================/\\");
        }

        // foreach (InputControl i in System_InputSystemLocator.InputSystem.Gameplay.Movement.controls)//Gameplay.MainInteract.bindings)
        // {
        //     // if (i.name.ToLower().Equals("w"))
        //     // {
        //     //     InputAction action = System_InputSystemLocator.InputSystem.Gameplay.Movement.actionMap.FindAction(
        //     //         i.name,
        //     //         throwIfNotFound : true);
        //     //     Debug.Log(action is null);
        //     //     var rebindOperation = action.PerformInteractiveRebinding()
        //     //         .WithControlsExcluding("Mouse")
        //     //         .OnMatchWaitForAnother(0.1f)
        //     //         .Start();
        //     // }

        //     Debug.Log(i.name);
        //     // Debug.Log(i.aliases);
        //     // Debug.Log(i.displayName);
        //     Debug.Log(i.path);
        // }

        // foreach (InputBinding i in System_InputSystemLocator.InputSystem.Gameplay.Get())//Gameplay.MainInteract.bindings)
        //     act(i);
        // foreach (InputBinding i in System_InputSystemLocator.InputSystem.Gameplay.AltInteract.bindings)
        //     act(i);

        // foreach (InputBinding i in System_InputSystemLocator.InputSystem.Gameplay.Movement.bindings)
        //     act(i);
        // foreach (InputBinding i in System_InputSystemLocator.InputSystem.Gameplay.MenuButton.bindings)
        //     act(i);
    }
}
