using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvGUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] Icons;

    public void UpdateVisuals(Inventory slots) // todo make adaptive
    {
        IEnumerator slotsEnumerator = slots.GetEnumerator();
        
        foreach (SpriteRenderer child in Icons)
        {
            if (slotsEnumerator.MoveNext() && slotsEnumerator.Current != null)
            {
                child.transform.parent.gameObject.SetActive(true);
                child.sprite = ((Item)slotsEnumerator.Current).Info.icon;
            }
            else
            {
                child.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
