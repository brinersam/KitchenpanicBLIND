using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioSO", menuName = "SObjects/ScenarioBase" )]
public class GameDifficultyScenarioSO : ScriptableObject
{
    [SerializeField] private RecipeSO[] _recipeArr;
    [SerializeField] private int _initialSeconds = 30;
    [SerializeField] [Range(0,100)] private int _randomTickDish = 3;
    [SerializeField] [Range(0,100)] private int _forceRecipeAt = 25;
    [SerializeField] [Range(0.1f,100)] private float _pts_Mod = 1.5f;

    public RecipeSO[] RecipeArr => _recipeArr;
    public int InitialSeconds => _initialSeconds;
    public double RandomTickDish_pct => _randomTickDish * 0.01;
    public double ForceRecipeAt_pct => _forceRecipeAt * 0.01;
    public float Pts_Mod => _pts_Mod;
}
