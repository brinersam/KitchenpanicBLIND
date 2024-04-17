using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(IsInteractable))]
public class IsItemDisposer : MonoBehaviour, IInteractable
{
    private AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void Interact(MonoBehaviour caller, bool alt)
    {
        Inventory callerInv = (caller as IInventory).Inventory;
        
        if (callerInv.Item_Peek() is null)
            return;
            
        callerInv.Item_Lose();
        System_Audio.Instance.PlaySoundOfType(SoundType.Trash ,audioSrc);
            
    }
}
