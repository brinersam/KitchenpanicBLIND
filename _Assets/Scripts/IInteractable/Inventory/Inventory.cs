using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{   
    [SerializeField] private int invSlots = 1;
    private LimitedSizeStack<Holdable> dataStack;

    [SerializeField] private bool hasWhiteList = false;
    [SerializeField] private ItemType[] item_WhiteList; // needs to be generic or bunsh of arrays with each having their own type
    [SerializeField] private HasItemDisplayer displayer;
    readonly HashSet<ItemType> _itemWhiteList;
    public bool IsFull => dataStack.IsFull;
    public bool IsEmpty => dataStack.IsEmpty;

    void Awake()
    {
        if (invSlots == 0) invSlots++; // remove when unity decides to stop being silly todo

        dataStack = new(invSlots);

        foreach(var i in item_WhiteList)    
            _itemWhiteList.Add(i);
        
        displayer.UpdateVisuals(dataStack);
    }

    public void Pull_PushItemFrom(Inventory giverInventory)
    {
        // Debug.Log("Entering pushpull"); // war is hell
        // Holdable topmostItem = giverInventory.Item_Peek();
        // if (topmostItem != null && topmostItem.type == ItemType.Container)
        // {   
        //     if (topmostItem.inventory == null)
        //         topmostItem.GiveInventory(10);

        //     giverInventory = topmostItem.inventory;
        //     Debug.Log("topmost is a plate!");
        // }
        
        if (!giverInventory.IsEmpty) // null exception if plate
        {
            this.Item_TryReceiveFrom(giverInventory);
            return;
        }

        giverInventory.Item_TryReceiveFrom(this);
    }

    public void Item_Replace(Holdable item)
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

    public bool Item_TryReceive(Holdable item)
    {
        if (item == null || !PassesWhitelist(item)) return false;

        bool success = dataStack.TryPush(item);
        displayer.UpdateVisuals(dataStack);

        if (success) Debug.Log($"Receoved item: {item.name}");

        return success;
    }

    public Holdable Item_Lose()
    {
        Holdable temp = dataStack.Pop();
        displayer.UpdateVisuals(dataStack);
        return temp;
    }
    
    public Holdable Item_Peek()
    {
        return dataStack.Peek();
    }

    private bool PassesWhitelist(Holdable item)
    {
        if (!hasWhiteList) return true;

        return _itemWhiteList.Contains(item.type);
    }
}
