using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Custom.InputSystemTools
{
    public static class InSysTools
    {
        public static bool TryGetAction(this InputBinding inputBinding, out InputAction inputAction)
        {
            inputAction = null;
            if (System_InputSystemLocator.InputSystem.FindBinding(inputBinding, out inputAction) != 0)
            {
                Debug.LogError($"No action found for binding: {inputBinding.path}");
                return false;
            }
            return true;
        }
    

        public static void BindControlGroup(IEnumerator<InputBinding> enum_i_InputBinding, List<BindingButton> buttonList)
        {
            IEnumerator<BindingButton> enum_j_Buttons = buttonList.GetEnumerator();

            while (enum_i_InputBinding.MoveNext() && enum_j_Buttons.MoveNext())
            {
                InputBinding i = enum_i_InputBinding.Current;
                BindingButton j = enum_j_Buttons.Current;
                j.Initialize(i);
            }
        }
    }


}