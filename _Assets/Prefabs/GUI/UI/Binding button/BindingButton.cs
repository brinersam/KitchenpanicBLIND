using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
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

    public void UpdateVisuals()
    {
        _inputBinding.TryGetAction(out InputAction inputAction);
        _bindingText.text = inputAction.controls[0].displayName;
    }

    private void OnMouseDown()
    {
        _bindingText.text = "...";
        this.Rebind(_inputBinding);
        Debug.Log($"{_bindingText.text} is being rebound...");
    }
}
