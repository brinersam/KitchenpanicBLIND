using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InvGUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] Icons;

    public void UpdateVisuals(IEnumerable<ItemInfo> collection)
    {
        IEnumerator<ItemInfo> itemIEnum = collection.GetEnumerator();

        foreach (SpriteRenderer icon in Icons)
        {
            if (itemIEnum.MoveNext() && itemIEnum.Current != null)
            {
                icon.transform.parent.gameObject.SetActive(true);
                icon.sprite = itemIEnum.Current.icon;
            }
            else
            {
                icon.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
