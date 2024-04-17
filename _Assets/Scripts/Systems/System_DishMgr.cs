using System;
using System.Collections.Generic;
using UnityEngine;

public static class System_DishMgr
{
    private static int _maximumDishes = SystemsHelper.MAX_RECIPES_IN_QUEUE;

    private static readonly List<RecipeSO> _recipeQueue = new(_maximumDishes);
    
    private static SystemsHelper _helper = null;

    public static event Action<RecipeSO> OnRecipeAdded;
    public static event Action<RecipeSO> OnRecipeRemoved;
    public static int MaximumDishes => _maximumDishes;
    public static List<RecipeSO> RecipeQueue => _recipeQueue;
    public static SystemsHelper Helper 
    {   
        get => _helper; set {InitialzeHelper(value);}
    }


    public static void OnTick()
    {
        if (_recipeQueue.Count < _maximumDishes * System_Session.Scenario.ForceRecipeAt_pct)
        {
            RequestNewDish();
        }
    }

    public static void OnTickEvent()
    {
        if (_recipeQueue.Count < _maximumDishes &&
            UnityEngine.Random.Range(0,100) * 0.01 < System_Session.Scenario.RandomTickDish_pct)
        {
            Debug.Log("RANDOM DISH");
            RequestNewDish();
        }
    }
    
    public static bool TryAcceptDish(Item plate)
    {
        int plateHash = plate.GetHashCode();

        Debug.Log($"Comparing hash of dish on the plate {plateHash} to:");
        for (int idx = 0; idx < _recipeQueue.Count; idx++)
        {
            Debug.Log($"            Recipe : {_recipeQueue[idx].recipeName}; Hash: {_recipeQueue[idx].GetHashCode()}");
            if (plateHash == _recipeQueue[idx].GetHashCode())
            {
                var temp = _recipeQueue[idx];
                _recipeQueue.RemoveAt(idx);
                AcceptRecipe(temp);
                return true;
            }
        }

        //DenyRecipe();
        return false;
    }

        
    private static void RequestNewDish()
    {
        if (_recipeQueue.Count >= _maximumDishes)
        {
            Debug.Log("Attempt at adding recipe while queue is full!");
            return;
        }

        RecipeSO randomRecipe = System_Session.Scenario.RecipeArr[UnityEngine.Random.Range(0,System_Session.Scenario.RecipeArr.Length)];

        _recipeQueue.Add(randomRecipe);
        Debug.Log($"Dishmgr: new recipe: {randomRecipe}");
        OnRecipeAdded?.Invoke(randomRecipe);
    }

    private static void AcceptRecipe(RecipeSO recipe)
    {
        Debug.Log($"Success!! Player gets +{(int)(recipe.SecToPrepare * System_Session.Scenario.Pts_Mod)} seconds!");
        
        System_Session.TimeCur += (int)(recipe.SecToPrepare * System_Session.Scenario.Pts_Mod);
        OnRecipeRemoved?.Invoke(recipe);
    }

    // private static void DenyRecipe()
    // {
    //     return;
    // }

    private static void Bind()
    {
        System_Tick.OnTick += OnTick;
        System_Tick.OnTickEvent += OnTick;
    }

    private static void InitialzeHelper(SystemsHelper val)
    {
        if (_helper != null)
        {   Debug.LogError("Interrupted attempt at second helper connecting to static @System_DishManager",val.gameObject);
            return;
        }

        Bind();
        _helper = val;
    }
}
