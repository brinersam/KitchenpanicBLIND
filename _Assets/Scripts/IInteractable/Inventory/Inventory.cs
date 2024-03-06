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
        dataStack = new(invSlots);

        foreach(var i in item_WhiteList)    
            _itemWhiteList.Add(i);
        
        //foreach(var i in dataStack)
        displayer.UpdateVisuals(dataStack);
    }

    public void Pull_PushItem(Inventory giverInventory) //honestly just split it into push and pull man this sucks todo
    {
        if (giverInventory.IsFull == this.IsFull) // bug todo if shelf has more than 1 slot and u put a thing in it, we fall into collision for some reason instead of interaction
        {
            if (IsEmpty) return;

            Debug.Log($"Inventories collided between {gameObject.name} && {giverInventory.gameObject.name}", gameObject);
            return;
        }

        if (giverInventory.IsFull)
        {
            this.Item_TryReceive(giverInventory);
            return;
        }

        if (this.IsFull)
        {
            giverInventory.Item_TryReceive(this);
            return;
        }
    }

    public void Item_Replace(Holdable item)
    {
        dataStack.Pop();
        dataStack.TryPush(item);
        //displayer.UpdateVisuals(dataStack.Peek());
        displayer.UpdateVisuals(dataStack);
    }


    public void Item_TryReceive(Inventory giverInv)
    {
        if (this.Item_TryReceive(giverInv.Item_Peek()))
            giverInv.Item_Lose();
    }

    public bool Item_TryReceive(Holdable item)
    {
        if (!PassesWhitelist(item)) return false;

        bool success = dataStack.TryPush(item);
        //displayer.UpdateVisuals(dataStack.Peek());
        displayer.UpdateVisuals(dataStack);

        return success;
    }

    public Holdable Item_Lose()
    {
        Holdable temp = dataStack.Pop();
        //displayer.UpdateVisuals(dataStack.Peek());
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
