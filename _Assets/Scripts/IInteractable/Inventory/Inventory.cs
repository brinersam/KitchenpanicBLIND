using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class Inventory : MonoBehaviour, IEnumerable<Item>
{   
    [SerializeField] private int invSlots = 1;
    [SerializeField] private bool hasWhiteList = false;
    [SerializeField] private ItemType[] item_WhiteList; // needs to be generic or bunsh of arrays with each having their own type

    private LimitedSizeStack<Item> dataStack;
    private ItemDisplayer displayer;
    readonly HashSet<ItemType> _itemWhiteList;
    public bool IsFull => dataStack.IsFull;
    public bool IsEmpty => dataStack.IsEmpty;

    public Inventory (int invCapacity)
    {
        if (invCapacity <= 0)
            throw new Exception("Attempt at initializing inventory of 0 slots!");

        invSlots = invCapacity;
        dataStack = new(invSlots);
    }

    void Awake()
    {
        TryGetComponent(out ItemDisplayer disp);
        displayer = disp;

        dataStack = new(invSlots);

        foreach(var i in item_WhiteList)    
            _itemWhiteList.Add(i);
        
        displayer.UpdateVisuals(this);
    }

    public void ItemPullPushFrom(Inventory giverInventory)
    {
        // Item topmostItem = giverInventory.Item_Peek();
        // if (topmostItem != null && topmostItem.Info.type == ItemType.Container) // i give up, this is too spaghetti i should follow the course for this one, hopefully i dont have to scrap too much code
        // {   
        //     // giverInventory = topmostItem.Inventory;
        //     // giverInventory.displayer = this.displayer;
        //     // return;
        // }
        
        if (!giverInventory.IsEmpty && this.Item_TryReceiveFrom(giverInventory))
        {
            return;
        }

        giverInventory.Item_TryReceiveFrom(this);
    }

    public void Item_Replace(Item item)
    {
        dataStack.Pop();
        dataStack.TryPush(item);
        displayer.UpdateVisuals(this);
    }

    public bool Item_TryReceiveFrom(Inventory giverInv)
    {
        bool success = Item_TryReceive(giverInv.Item_Peek());
        if (success)
        {
            giverInv.Item_Lose();
        }
        return success;
    }

    public bool Item_TryReceive(Item item)
    {
        if (item == null || !PassesWhitelist(item)) return false;

        bool success = dataStack.TryPush(item);
        if (displayer != null) displayer.UpdateVisuals(this);
        return success;
    }

    public Item Item_Lose()
    {
        Item temp = dataStack.Pop();
        if (displayer != null) displayer.UpdateVisuals(this);
        return temp;
    }
    
    public Item Item_Peek()
    {
        return dataStack.Peek();
    }

    private bool PassesWhitelist(Item item)
    {
        if (!hasWhiteList) return true;

        return _itemWhiteList.Contains(item.Info.type);
    }

    public IEnumerator<Item> GetEnumerator()
    {
        return dataStack.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
