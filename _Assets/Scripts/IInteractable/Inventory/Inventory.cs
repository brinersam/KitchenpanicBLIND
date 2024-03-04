using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{   
    [SerializeField] private IHoldable[] slots = new IHoldable[1];
    [SerializeField] private bool hasWhiteList = false;
    [SerializeField] private ItemType[] item_WhiteList;
    [SerializeField] private ItemDisplayer displayer;
    readonly HashSet<ItemType> _itemWhiteList;
    public bool IsEmpty {get {return this[0] == null;}}
    public IHoldable this[int idx]
    {
        get { return slots[idx]; }
        set {
                slots[idx] = value;
                displayer.UpdateVisuals(this[idx]);
            }
    }
    
    private void Awake()
    {
        foreach(var i in item_WhiteList)    
            _itemWhiteList.Add(i);
    }

    public void TryReceiveItem(IHoldable item, int idx = 0)
    {
        if (hasWhiteList && !_itemWhiteList.Contains(item.type)) //dry todo
        {
            Debug.LogError($"Attempt at receiving incompatible item", gameObject);
            return;
        }

        if (!IsEmpty)
        {
            Debug.LogError($"Attempt at receiving an item while inventory is full!", gameObject);
            return;
        }
        this[0] = item;
    }
    public void TryReceiveItem(Inventory giverInventory, int idx = 0)
    {
        if (giverInventory.IsEmpty) // should i use command pattern instead of all these checks?? o_O
        {
            Debug.LogError($"Attempt at receiving NULL item", gameObject);
            return;
        }

        if (hasWhiteList && !_itemWhiteList.Contains(giverInventory[0].type))  //dry todo
        {
            Debug.LogError($"Attempt at receiving incompatible item", gameObject);
            return;
        }

        if (!IsEmpty) // add check for whether two items are interactable (plate + things) todo
        {
            Debug.LogError($"Attempt at receiving an item while inventory is full!", gameObject);
            return;
        }

        this[0] = giverInventory.LoseItem();
    }

    public IHoldable LoseItem()
    {
        IHoldable temp = this[0];
        this[0] = null;
        return temp;
    }
}
