using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InvGUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] Icons;

    public void UpdateVisuals(List<RecipeSO> recipeList)
    {
        // todo errors when nothing in queue
        // make a separate class for system ui tbh
        UpdateVisuals(recipeList[0].ingredients);
    }

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
