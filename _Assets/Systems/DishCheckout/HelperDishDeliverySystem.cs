using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperDishDeliverySystem : MonoBehaviour
{
    [SerializeField] private MonoBehaviour timerUIscript;
    public RecipeSO[] recipesArr;
    void Awake()
    {
        DishDeliverySystem.Helper = this;
    }
}
