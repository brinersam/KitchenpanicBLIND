using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DishDeliverySystem
{
    public const int MAXIMUM_DISHES = 5;

    private static HelperDishDeliverySystem helper = null;
    public static HelperDishDeliverySystem Helper 
    {
        get
        { return helper; }
        set
        {
            if (helper != null)
            {
                Debug.LogError("Interrupted attempt at second helper trying to connect to static@DishDeliverySystem",value.gameObject);
                return;
            }
            helper = value;
        }
    }

    public static bool TryAcceptDish(Item plate)
    {
        bool recipeAccepted = false;
        int plateHash = RecipeHashFun.GetHash(plate);

        Debug.Log($"{plate.Info.name} hash : {plateHash} being compared to:");

        foreach (var recipe in helper.recipesArr) // todo switch to current requests instead of just the ones available
        {
            Debug.Log($"                {recipe.recipeName} hash : {RecipeHashFun.GetHash(recipe)}");

            if (recipeAccepted) break;

            // if (plate.GetHashCode() == recipe.GetHashCode()) // todo use running hash if this is expensive
            if (plateHash == RecipeHashFun.GetHash(recipe))
            {
                recipeAccepted = true;
            }
        }
        
        Debug.Log(recipeAccepted? "dish accepted!" : "dish denied");
        return recipeAccepted;
    }


    public static RecipeSO RequestNewDish(RecipeRarity forcedRarity)
    {
        return helper.recipesArr[0];
    }
        

}
