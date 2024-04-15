using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Custom.InputSystemTools
{
    public static class InputSystemTools
    {
        public static void Rebind(this BindingButton btn, InputBinding inputBinding)
        {
            inputBinding.TryGetAction(out InputAction inputAction);
            inputAction.Disable(); 
            var rebindOperation = inputAction.PerformInteractiveRebinding()
                        .WithControlsExcluding("Mouse")
                        .WithCancelingThrough("<Keyboard>/escape")
                        .Start()
                        .OnComplete((x) =>
                        {
                            Debug.Log($"rebind success!");
                            btn.UpdateVisuals();
                            inputAction.Enable();

                            x.Dispose();
                        })
                        .OnCancel((x)=>
                        {
                            Debug.Log($"rebind cancelled!!");
                            btn.UpdateVisuals();
                            inputAction.Enable();

                            x.Dispose();
                        });
        }

        public static bool TryGetAction(this InputBinding inputBinding, out InputAction inputAction)
        {
            inputAction = null;
            //if (System_InputSystemLocator.InputSystem.FindBinding(inputBinding, out inputAction) != 0) // WRONG!!!!!! NO NO NO DONT USE InputSystem CLASS
            // ok so you need to understand that you must locate the binding from fucking asset instead of the class
            // why?
            // ways of getting the class:
            //   checkmark - generates the class and also generates a new fucking class at runtime so you get two??? generate it once i guess and forget about it
            //   editor drag and drop - good luck, the class doesnt inherit from Mono and has no interface so u cant cast it 
            //         (maybe DI container works but same as above - its probably the wrong class instance)
            //   new - what i use in order to connect class to the asset through binding, 
            //   doesnt work on its own though (despite looking like it works in debug tool)
            if (System_InputSystemLocator.InputActionAsset.FindBinding(inputBinding, out inputAction) != 0)
            {
                Debug.LogError($"No action found for binding: {inputBinding.path}");
                return false;
            }
            return true;
        }
    

        public static void BindControlGroup(IEnumerator<InputBinding> enum_i_InputBinding, List<BindingButton> buttonList)
        {   // todo composite support
            IEnumerator<BindingButton> enum_j_Buttons = buttonList.GetEnumerator();

            while (enum_i_InputBinding.MoveNext() && enum_j_Buttons.MoveNext())
            {
                InputBinding i = enum_i_InputBinding.Current;
                BindingButton j = enum_j_Buttons.Current;
                j.Initialize(i);
            }
        }

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


}