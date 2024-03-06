using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemDisplayer))]
public class Inventory : MonoBehaviour
{   
    [SerializeField] private IHoldable[] slots = new IHoldable[1]; // use stack instead or smth
    [SerializeField] private bool hasWhiteList = false;
    [SerializeField] private ItemType[] item_WhiteList;
    [SerializeField] private ItemDisplayer displayer;
    readonly HashSet<ItemType> _itemWhiteList;
    public bool IsFull {get {return slots.FirstOrDefault(x => x != null) == null;}} // todo

    public IHoldable this[int idx]
    {
        get { return slots[idx]; }
        set {
                slots[idx] = value;
                displayer.UpdateVisuals(this[idx]);
            }
    }
    
    void Awake()
    {
        foreach(var i in item_WhiteList)    
            _itemWhiteList.Add(i);
        
        foreach(var i in slots)
            if (i != null)
                displayer.UpdateVisuals(i);
    }

    public void InteractWInv(Inventory giverInventory, int idx = 0)
    {
        if (giverInventory.IsFull == IsFull)
        {
            if (IsFull) return;

            Debug.LogError($"Inventories collided", gameObject);
        }

        else if (this.IsFull)
        {
            giverInventory.TryReceiveItem(this, idx);
        }

        else if (giverInventory.IsFull)
        {
            this.TryReceiveItem(giverInventory, idx);
        }
    }

    public void TryReceiveItem(IHoldable item, int idx = 0)
    {
        if (IsFull) {return;}

        if (PassesWhitelist(item))
            this[idx] = item;
    }

    public void TryReceiveItem(Inventory callerInv, int idx = 0)
    {
        if (IsFull)
        {
            Debug.LogError($"Attempt at receiving an item while inventory is full! This shouldn't happen.", gameObject);
            return;
        }
        
        if (PassesWhitelist(callerInv[idx]))
            this[idx] = callerInv.LoseItem(idx);
    }

    public IHoldable LoseItem(int idx = 0)
    {
        IHoldable temp = this[idx];
        this[idx] = null;
        return temp;
    }

    private bool PassesWhitelist(IHoldable item)
    {
        if (!hasWhiteList) return true;

        return _itemWhiteList.Contains(item.type);
    }
}
