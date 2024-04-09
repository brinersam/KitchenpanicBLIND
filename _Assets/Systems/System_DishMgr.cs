using System;
using System.Collections.Generic;
using UnityEngine;

public static class System_DishMgr
{
    private static int maximumDishes = SystemsHelper.MAX_RECIPES_IN_QUEUE;

    private static readonly List<RecipeSO> recipeQueue = new(maximumDishes);
    
    private static SystemsHelper _helper = null;
    public static SystemsHelper Helper 
    {   
        get => _helper;
        set {SetHelper(value);}
    }

    public static List<RecipeSO> RecipeQueue => recipeQueue;
    public static event Action<RecipeSO> OnRecipeAdded;
    public static event Action<RecipeSO> OnRecipeRemoved;
    public static int MaximumDishes => maximumDishes;


    public static void OnTick()
    {
        if (recipeQueue.Count < maximumDishes &&
            UnityEngine.Random.Range(0,100) * 0.01 < System_Difficulty.Scenario.RandomTickDish_pct)
        {
            Debug.Log("EVENT DISH");
            RequestNewDish();
        }
    }

    public static void OnTickEvent()
    {
        if (recipeQueue.Count < maximumDishes * System_Difficulty.Scenario.GenerateRecipeWhenQueueFullAt_pct)
        {
            RequestNewDish();
        }
    }
    
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

        
    private static void RequestNewDish()
    {
        if (recipeQueue.Count >= maximumDishes)
        {
            Debug.Log("Attempt at adding recipe while queue is full!");
            return;
        }

        RecipeSO randomRecipe = System_Difficulty.Scenario.recipeArr[UnityEngine.Random.Range(0,System_Difficulty.Scenario.recipeArr.Length)];

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

    private static void Bind()
    {
        System_Tick.OnTick += OnTick;
        System_Tick.OnTickEvent += OnTick;
    }

    private static void SetHelper(SystemsHelper val)
    {
        if (_helper != null)
        {   Debug.LogError("Interrupted attempt at second helper connecting to static @System_DishManager",val.gameObject);
            return;
        }
        else
        {
            Bind();
        }
        _helper = val;
    }
}
