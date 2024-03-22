using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "SObjects/RecipeBase" )]
public class RecipeSO : ScriptableObject
{
    const float DIFFICULTY_TIME_MOD = 1.5f;

    public string recipeName;
    public RecipeRarity rarity;

    public ItemInfo[] ingredients; // make a serializable struct so it can be displayed in the editor todo
    public int[] amountEach; //  -----^

    [SerializeField] private int approxSecToPrepare = -1;

    private int approxSecToPrepareCalculated = 0;
    private int hash;

    public int SecToPrepare { 
        get {
            if (approxSecToPrepare == -1)
                return approxSecToPrepareCalculated;

            return approxSecToPrepare;
        }
    }

    void OnEnable() 
    {
        if (approxSecToPrepare == -1)
            CalculateTime();

        if (hash == default)
            RecalculateHash();
    }

    public override int GetHashCode()
    {
        return hash;
    }

    private void CalculateTime()
    {
        float resultTime = 0;

        for(int idx = 0; idx < ingredients.Length; idx++)
        {
            if (ingredients[idx] == null) break;

            int CorHReq = CalculateIngredientTime(ingredients[idx]);
            resultTime += CorHReq * amountEach[idx] * DIFFICULTY_TIME_MOD;
        }

        if (resultTime == 0)
            Debug.LogWarning($"Recipe \"{this.recipeName}\" is expected to take 0 sec to complete!",this);

        approxSecToPrepareCalculated = (int)Mathf.Ceil(resultTime);
    }

    private int CalculateIngredientTime(ItemInfo ingr)
    {
        int CorHReq = ingr.ChopsOrHeatsRequired;
        switch (ingr.prepareMethod)
        {
            case PreparationEnum.None:
            {
                CorHReq = 5; 
                break;
            }
            case PreparationEnum.Chop:
            {
                CorHReq /= 3; // we expect player to be able to chop 3 times per seconds
                break;
            }
        }
        return CorHReq;
    }

    private void RecalculateHash()
    {
        hash = RecipeHashClass.ExtractHash(ingredients,itemAmountArray: amountEach);

        if (hash == default)
            Debug.LogWarning($"Hash for recipe {this.recipeName} is recalculated to a zero!", this);
    }
}
