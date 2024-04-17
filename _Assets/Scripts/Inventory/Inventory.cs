using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class Inventory : MonoBehaviour, IEnumerable<Item>, IEnumerable<ItemInfo>
{   
    [SerializeField] private int invSlots = 1;
    [SerializeField] private bool _playsSounds = false;
    [SerializeField] private ItemType[] item_WhiteList; // needs to be generic or bunch of arrays with each having their own type

    private LimitedSizeStack<Item> dataStack;
    private ItemDisplayer displayer;
    readonly private HashSet<ItemType> _itemWhiteList;

    public bool PlaysSounds => _playsSounds;
    public bool IsFull => dataStack.IsFull;
    public bool IsEmpty => dataStack.IsEmpty;
    public ItemDisplayer Displayer => displayer;
    private bool HasWhiteList => _itemWhiteList != null && _itemWhiteList.Any();

    public Inventory (int invCapacity, ItemType[] whitelist = null)
    {
        if (invCapacity <= 0)
            throw new Exception("Attempt at initializing inventory of 0 slots!");
        
        if (whitelist != null)
        {
            _itemWhiteList = new();
            foreach(var i in whitelist)    
                _itemWhiteList.Add(i);
        }

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
        
        if (!this.IsEmpty)
            UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        displayer.UpdateVisuals(this);
    }

    public void Item_PullPushFrom(Inventory giverInventory)
    {
        if (!giverInventory.IsEmpty && this.Item_TryReceiveFrom(giverInventory))
        {
            return;
        }

        giverInventory.Item_TryReceiveFrom(this);
    }

    public void Item_Replace(Item item)
    {
        dataStack.ReplaceTop(item);
        UpdateVisuals();
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

        if (success && _playsSounds)
            System_Audio.Instance.PlaySoundOfType(SoundType.Object_pick);

        if (displayer != null) UpdateVisuals();;
        return success;
    }

    public Item Item_Lose()
    {
        Item temp = dataStack.Pop();
        if (displayer != null) UpdateVisuals();

        if (temp is null == false && _playsSounds)
            System_Audio.Instance.PlaySoundOfType(SoundType.Object_drop);

        return temp;
    }
    
    public Item Item_Peek()
    {
        return dataStack.Peek();
    }

    private bool PassesWhitelist(Item item)
    {
        if (!HasWhiteList) return true;

        return _itemWhiteList.Contains(item.Info.Type);
    }

    public IEnumerator<Item> GetEnumerator()
    {
        return dataStack.GetEnumerator();
    }

    private IEnumerator<ItemInfo> GetEnumerator_ItemInfo()
    {
        foreach(Item item in dataStack)
        {
            yield return item.Info;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator<ItemInfo> IEnumerable<ItemInfo>.GetEnumerator()
    {
        return GetEnumerator_ItemInfo();
    }
}
