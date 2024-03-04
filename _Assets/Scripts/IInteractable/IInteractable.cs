using UnityEngine;
public interface IInteractable
{
    void Interact(PlayerCursor caller, bool alt);
}