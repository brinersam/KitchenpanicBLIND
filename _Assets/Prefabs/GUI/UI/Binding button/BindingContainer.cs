using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BindingContainer : MonoBehaviour
{
    //[SerializeField] private TMP_Text bindingText;
    //[SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private ControlGroupsEnum _controlType;
    [SerializeField] private List<BindingButton> _buttons;
    
    public ControlGroupsEnum ControlGroup => _controlType;
    public List<BindingButton> Buttons => _buttons;

    private void Awake()
    {
        foreach (Transform child in buttonContainer)    
        {
            _buttons.Add(child.GetComponent<BindingButton>());
        }
    }
}
