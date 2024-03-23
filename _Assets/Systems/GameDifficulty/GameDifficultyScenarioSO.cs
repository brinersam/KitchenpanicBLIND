using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioSO", menuName = "SObjects/ScenarioBase" )]
public class GameDifficultyScenarioSO : ScriptableObject
{
    public RecipeSO[] recipeArr;
    public int initialSeconds = 30;
    [Range(0,100)] public int randomTickDish_pct = 3;
    [Range(0,100)] public int generateRecipeWhenQueueFullAt_pct = 25;
    public float recipeCompletionPts_mod = 1.5f;
}
