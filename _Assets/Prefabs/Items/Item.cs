using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IInventory
{
    private ItemInfo info;
    private readonly Inventory inventory;
    public Inventory Inventory => inventory;
    public ItemInfo Info => info;

    public Item(ItemInfo info)
    {
        this.info = info;
        if (info.invCapacity > 0)
            {
                inventory = new Inventory(info.invCapacity);
            }
    }

    public bool TryReturnPreparedVersion(out Item result)
    {
        if (info.prepareMethod == PreparationEnum.None)
        {
            result = null;
            return false;
        }

        result = this.info.prepareResult.SpawnItem();
        return true;
    }

    public void BecomePrepared()
    {
        if (info.prepareMethod == PreparationEnum.None) return;

        this.info = Info.prepareResult;
    }
}
