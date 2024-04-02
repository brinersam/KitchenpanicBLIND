using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioSO", menuName = "SObjects/ScenarioBase" )]
public class GameDifficultyScenarioSO : ScriptableObject
{
    public RecipeSO[] recipeArr;
    public int initialSeconds = 30;
    [SerializeField] [Range(0,100)] private int randomTickDish = 3;
    [SerializeField] [Range(0,100)] private int generateRecipeWhenQueueFullAt = 25;
    public float recipeCompletionPts_mod = 1.5f;


    public double RandomTickDish_pct => randomTickDish * 0.01;
    public double GenerateRecipeWhenQueueFullAt_pct => generateRecipeWhenQueueFullAt * 0.01;
}
