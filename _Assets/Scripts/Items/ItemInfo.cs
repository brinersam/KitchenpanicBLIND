using System;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemInfoSO", menuName = "SObjects/ItemBase" )]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private GameObject visualObj;
    [SerializeField] private Sprite sprite;
    [SerializeField] private ItemType type;

    public GameObject VisualObj => visualObj;
    public Sprite Sprite => sprite;
    public ItemType Type => type;

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
