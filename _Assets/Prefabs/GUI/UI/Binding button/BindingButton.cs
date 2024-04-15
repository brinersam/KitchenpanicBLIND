using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;
using Custom.InputSystemTools;

public class BindingButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _bindingText;
    private InputBinding _inputBinding;
    public TMP_Text BindingText => _bindingText;
    
    public void Initialize(InputBinding inputBinding)
    {
        _inputBinding = inputBinding;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        _inputBinding.TryGetAction(out InputAction inputAction);
        _bindingText.text = inputAction.controls[0].displayName;
    }

    private void OnMouseDown() // literally works but not, im not wasting any more time on yet another unitys shit docs
    {// todo refactor int otis own class or whatever
        _inputBinding.TryGetAction(out InputAction inputAction);

        inputAction.Disable(); 
        Debug.Log($"{_bindingText.text} is being rebound...");
        RebindingOperation rebindOperation = inputAction.PerformInteractiveRebinding() // seems to work judging by their debugger, but controls stay the same lol
                    .WithControlsExcluding("Mouse")
                    .WithCancelingThrough("<Keyboard>/escape")
                    .WithTargetBinding(0) // i have both inputbinding and inputaction yet no way to actually get the index, nice code design
                    .WithBindingGroup(_inputBinding.groups)
                    .OnComplete((x) =>
                    {
                        Debug.Log($"rebind success!");
                        UpdateVisuals();
                        inputAction.Enable();

                        x.Dispose();
                    })
                    .OnCancel((x)=>
                    {
                        Debug.Log($"rebind cancelled!!");
                        UpdateVisuals();
                        inputAction.Enable();

                        x.Dispose();
                    });
        rebindOperation.Start();
    }
}
