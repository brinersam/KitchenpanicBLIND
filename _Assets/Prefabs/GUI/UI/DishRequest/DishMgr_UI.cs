using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishMgr_UI : MonoBehaviour
{
    private DishRequestBox[] recipeBoxes;
    private int CurCapIdx = 0;
    [SerializeField] private GameObject recipeBoxPrefab;

    private void Awake()
    {
        recipeBoxes = new DishRequestBox[System_DishMgr.MAXIMUM_DISHES];
        for (int i = 0; i < recipeBoxes.Length; i++)
        {
            var go = Instantiate(recipeBoxPrefab);
            go.transform.SetParent(transform,false);
            go.SetActive(false);
            recipeBoxes[i] = GetComponent<DishRequestBox>();
        }
    }

    private void Start() 
    {
        System_DishMgr.OnRecipeAdded += Recipe_Add;
        System_DishMgr.OnRecipeRemoved += Recipe_Remove;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("adding a recipe");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("removing a recipe in the middle");
        }
    }

    private void Recipe_Add()
    {
        Recipe_Add(CurCapIdx);
    }

    private void Recipe_Add(int idx)
    {
        if (idx >= recipeBoxes.Length)
        {
            Debug.LogWarning("Recipe list overflow prevented");
            return;
        }

        Debug.Log("recipe add"); //todo

        CurCapIdx++;
    }

    private void Recipe_Remove(int idx)
    {
        if (CurCapIdx - 1 < 0)
        {
            Debug.LogWarning("Recipe list underflow prevented");
            return;
        }

        Debug.Log("recipe remove"); //todo

        CurCapIdx--;
    }
}
