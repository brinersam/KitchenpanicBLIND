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
        int plateHash = plate.GetHashCode();

        Debug.Log($"Comparing hash of dish on the plate {plateHash} to:");
        foreach (var recipe in helper.recipesArr) // todo switch to current requests instead of just the ones available
        {
            Debug.Log($"            Recipe : {recipe.recipeName}; Hash: {recipe.GetHashCode()}");
            if (plateHash == recipe.GetHashCode())
            {
                recipeAccepted = true;
                break;
            }
        }
        
        Debug.Log(recipeAccepted? "dish accepted!" : "dish denied");
        return recipeAccepted;
    }


    public static RecipeSO RequestNewDish(RecipeRarity forcedRarity)
    {
        return helper.recipesArr[Random.Range(0,helper.recipesArr.Length)];
    }
        

}
