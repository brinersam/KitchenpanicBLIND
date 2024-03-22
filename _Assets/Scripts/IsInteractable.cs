using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private MeshRenderer meshToHighlight;
    [SerializeField] private MonoBehaviour IInteractableScript;

    private IInteractable InteractScript;

    private void Awake()
    {
        InteractScript = (IInteractable)IInteractableScript;
    }

    public void ToggleHighlight(bool selected)
    {
        MaterialPropertyBlock MPB = new();
        if (selected)
        {
            MPB.SetFloat("_HighLighted",1);
        }
        else
        {
            MPB.SetFloat("_HighLighted",0);
        }
        meshToHighlight.SetPropertyBlock(MPB);
    }

    public void Interact(MonoBehaviour caller, bool alt)
    {
        InteractScript.Interact(caller, alt);
    }
}
