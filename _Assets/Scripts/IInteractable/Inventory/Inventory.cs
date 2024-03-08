using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{   
    [SerializeField] private int invSlots = 1;
    private LimitedSizeStack<Item> dataStack;

    [SerializeField] private bool hasWhiteList = false;
    [SerializeField] private ItemType[] item_WhiteList; // needs to be generic or bunsh of arrays with each having their own type
    [SerializeField] public HasItemDisplayer displayer;
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
        dataStack = new(invSlots);

        foreach(var i in item_WhiteList)    
            _itemWhiteList.Add(i);
        
        displayer.UpdateVisuals(dataStack);
    }

    public void Pull_PushItemFrom(Inventory giverInventory)
    {
        Item topmostItem = giverInventory.Item_Peek();
        if (topmostItem != null && topmostItem.Info.type == ItemType.Container) // i give up, this is too spaghetti i should follow the course for this one, hopefully i dont have to scrap too much code
        {   
            // giverInventory = topmostItem.Inventory;
            // giverInventory.displayer = this.displayer;
            // return;
        }
        
        if (!giverInventory.IsEmpty)
        {
            this.Item_TryReceiveFrom(giverInventory);
            return;
        }

        giverInventory.Item_TryReceiveFrom(this);
    }

    public void Item_Replace(Item item)
    {
        dataStack.Pop();
        dataStack.TryPush(item);
        displayer.UpdateVisuals(dataStack);
    }

    public void Item_TryReceiveFrom(Inventory giverInv)
    {
        if (this.Item_TryReceive(giverInv.Item_Peek()))
            giverInv.Item_Lose();
    }

    public bool Item_TryReceive(Item item)
    {
        if (item == null || !PassesWhitelist(item)) return false;

        bool success = dataStack.TryPush(item);
        displayer.UpdateVisuals(dataStack);
        return success;
    }

    public Item Item_Lose()
    {
        Item temp = dataStack.Pop();
        displayer.UpdateVisuals(dataStack);
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
}
