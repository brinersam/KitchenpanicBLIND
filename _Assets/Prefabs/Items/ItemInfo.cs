using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Holdables", menuName = "Holdables/HoldableBase" )]
public class ItemInfo : ScriptableObject
{
    public GameObject visualObj;
    public Sprite icon;
    public ItemType type;

    public Item SpawnItem()
    {
        return new Item(this);
    }

/// PrepareMethod
    public PreparationEnum prepareMethod;
    public ItemInfo prepareResult;
    public int ChopsOrHeatsRequired = 6;

/// Inventory
    public int invCapacity = 0;
    public GameObject uiObj;
}
