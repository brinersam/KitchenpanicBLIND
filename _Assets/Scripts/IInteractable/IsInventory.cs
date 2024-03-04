using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInventory : MonoBehaviour
{   
    [SerializeField] private IHoldable[] Slots = new IHoldable[1];
    [SerializeField] private bool HasWhiteList = false;
    [SerializeField] private ItemType[] Item_WhiteList;
    readonly HashSet<ItemType> ItemWhiteList;

    public IHoldable this[int idx]
    {
        get { return Slots[idx]; }
        set { Slots[idx] = value; }
    }

    private void Awake()
    {
        foreach(var i in Item_WhiteList)    
            ItemWhiteList.Add(i);
    }

    public void TryReceiveItem(IsInventory giverInventory)
    {
        if (giverInventory[0] == null)
        {
            Debug.LogError($"Attempt at receiving NULL item", gameObject);
            return;
        }

        if (HasWhiteList && !ItemWhiteList.Contains(giverInventory[0].type))
        {
            Debug.LogError($"Attempt at receiving incompatible item", gameObject);
            return;
        }

        if (Slots[0] != null)
        {
            Debug.LogError($"Attempt at receiving an item while inventory is full!", gameObject);
        }

        Slots[0] = giverInventory.LoseItem();
    }

    public void TryToGiveItemTo(IsInventory receiverInventory)
    {
        if (Slots[0] == null)
        {
            Debug.LogError($"Attempt at giving a null item!", gameObject);
            return;
        }

        receiverInventory.TryReceiveItem(this);
    }

    public IHoldable LoseItem()
    {
        var temp = Slots[0];
        Slots[0] = null;
        return temp;
    }
        
}
