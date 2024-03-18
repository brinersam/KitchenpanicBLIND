using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperDishDeliverySystem : MonoBehaviour
{
    [SerializeField] private GameObject timerUI; // todo
    public RecipeSO[] recipesArr;
    void Awake()
    {
        DishDeliverySystem.Helper = this;
    }
}
