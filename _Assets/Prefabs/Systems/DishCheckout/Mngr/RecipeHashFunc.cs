using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RecipeHashFun
{
    public static int GetHash(RecipeSO recipe)
    {
        return ExtractHash(recipe.ingredients);
    }
    public static int GetHash(Item item)
    {
        return ExtractHash(item.Inventory, (x) => x.Info);
    }

    private static int ExtractHash<T>(IEnumerable<T> inv, Func<T,ItemInfo> fun = null)
    {
        int cumSum = 0;
        foreach (var i in inv)
        {
            unchecked
            {
                if (fun == null)
                    cumSum += i.GetHashCode(); 
                else
                    cumSum += fun(i).GetHashCode(); // Func
            }
        }
        return cumSum;
    }
}