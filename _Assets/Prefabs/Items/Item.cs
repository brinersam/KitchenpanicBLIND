using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IInventory
{
    private ItemInfo info;
    private Inventory inventory;
    public Inventory Inventory => inventory;
    public ItemInfo Info => info;
    public Item(ItemInfo info)
    {
        this.info = info;
        if (info.invCapacity > 0)
        {
            // todo turn Inventory into InventoryInternal(pure c#) and Inventory : MonoBehaviour (heavily relies on InventoryInternal)
            //ItemType[] plateWhiteList = new[]{ItemType.Ingredient_Prepared, ItemType.Ingredient_Trash}; // todo move this into iteminfo of plate
            inventory = new Inventory(info.invCapacity);// plateWhiteList);  // note for the future, it works but on field check returns null
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
