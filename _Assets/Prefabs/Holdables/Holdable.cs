using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Holdables", menuName = "Holdables/HoldableBase" )]
public class Holdable : ScriptableObject
{
    public GameObject visualObj;
    public Sprite icon;
    public ItemType type;

/// This line below should be implemented using composition but i cant link it on Start() or through editor (cant access fields of or insert a monobehaviour)
    public PreparationEnum prepareMethod;
    public Holdable prepareResult;
    public int ChopsOrHeatsRequired = 6;
    public bool TryPrepare(out Holdable result)
    {
        if (prepareMethod == PreparationEnum.None)
        {
            result = null;
            return false;
        }

        result = Instantiate(prepareResult);
        return true;
    }
}
