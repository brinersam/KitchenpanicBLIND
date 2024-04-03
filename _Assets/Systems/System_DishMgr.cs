using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class System_DishMgr
{
    public static int MAXIMUM_DISHES = SystemsHelper.MAX_RECIPES_IN_QUEUE;

    public static event Action<RecipeSO> OnRecipeAdded;
    public static event Action<RecipeSO> OnRecipeRemoved;

    public static event Action OnScenarioChange; // separate this into difficultySystem todo
    private static SystemsHelper helper = null;
    private static GameDifficultyScenarioSO __scenario; // separate this into difficultySystem todo
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
        if (recipeQueue.Count < MAXIMUM_DISHES &&
            UnityEngine.Random.Range(0,100) * 0.01 < Scenario.RandomTickDish_pct)
        {
            Debug.Log("EVENT DISH");
            RequestNewDish();
        }
    }

    public static void OnTickEvent()
    {
        if (recipeQueue.Count < MAXIMUM_DISHES * Scenario.GenerateRecipeWhenQueueFullAt_pct)
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

        RecipeSO randomRecipe = Scenario.recipeArr[UnityEngine.Random.Range(0,Scenario.recipeArr.Length)];

        recipeQueue.Add(randomRecipe);
        Debug.Log($"Dishmgr: new recipe: {randomRecipe}");
        OnRecipeAdded?.Invoke(randomRecipe);
    }

    private static void AcceptRecipe(RecipeSO recipe)
    {
        Debug.Log($"Success!! Player gets +{recipe.SecToPrepare} seconds!");
        OnRecipeRemoved?.Invoke(recipe);
    }

    private static void DenyRecipe()
    {
        Debug.Log($"UNACCEPTABLE!!");
    }
}
