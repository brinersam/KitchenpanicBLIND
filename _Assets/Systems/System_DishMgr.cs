using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class System_DishMgr
{
    public const int MAXIMUM_DISHES = 1;

    public static event Action OnScenarioChange;
    private static SystemsHelper helper = null;
    private static GameDifficultyScenarioSO __scenario;
    private static List<RecipeSO> recipeQueue = new(MAXIMUM_DISHES);

    public static SystemsHelper Helper 
    {   get => helper;
        set
        {if (helper != null)
            {   Debug.LogError("Interrupted attempt at second helper connecting to static @DishDeliverySystem",value.gameObject);
                return;
            }
            helper = value;
        }
    }
    public static GameDifficultyScenarioSO Scenario
    {   get => __scenario;
        set{OnScenarioChange?.Invoke();
            __scenario = value;
        }
    }
    public static List<RecipeSO> RecipeQueue => recipeQueue;

    public static bool TryAcceptDish(Item plate)
    {
        int plateHash = plate.GetHashCode();

        Debug.Log($"Comparing hash of dish on the plate {plateHash} to:");
        for (int idx = 0; idx < recipeQueue.Count; idx++)
        {
            Debug.Log($"            Recipe : {recipeQueue[idx].recipeName}; Hash: {recipeQueue[idx].GetHashCode()}");
            if (plateHash == recipeQueue[idx].GetHashCode())
            {
                var temp = recipeQueue[idx];
                recipeQueue.RemoveAt(idx);
                AcceptRecipe(temp);
                return true;
            }
        }

        DenyRecipe();
        return false;
    }

    public static void OnTick()
    {
        //update timer
        if (recipeQueue.Count < MAXIMUM_DISHES && UnityEngine.Random.Range(0,100) < Scenario.randomTickDish_pct)
        {
            RequestNewDish();
        }
    }

    public static void OnTickEvent()
    {
        if (recipeQueue.Count < MAXIMUM_DISHES && recipeQueue.Count < MAXIMUM_DISHES * Scenario.generateRecipeWhenQueueFullAt_pct)
        {
            RequestNewDish();
        }
    }
        
    private static void RequestNewDish()
    {
        if (recipeQueue.Count >= MAXIMUM_DISHES)
        {
            Debug.Log("Attempt at adding recipe while queue is full!");
            return;
        }
        recipeQueue.Add(Scenario.recipeArr[UnityEngine.Random.Range(0,Scenario.recipeArr.Length)]);
        Debug.Log($"Dishmgr: new recipe: {recipeQueue[0]}");
        System_UI.RefreshQueueUI();
    }

    private static void AcceptRecipe(RecipeSO recipe)
    {
        Debug.Log($"Success!! Player gets +{recipe.SecToPrepare} seconds!");
        System_UI.RefreshQueueUI();
    }

    private static void DenyRecipe()
    {
        Debug.Log($"UNACCEPTABLE!!");
    }
}
