using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DishRequestBox : MonoBehaviour
{
    private const int MAX_INGREDIENT_ICONS = 8;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _recipeIconsContainer;
    [SerializeField] private GameObject _iconPrefab;
    private SpriteRenderer[] _icons;

    private RecipeSO _lastData;
    public RecipeSO LastData => _lastData;

    private void Awake()
    {
        Init();
    }

    public void UpdateVisuals(DishRequestBox box)
    {
        UpdateVisuals(box.LastData);
    }
    public void UpdateVisuals(RecipeSO recipe)
    {
        if (recipe == null)
            return;
            
        _lastData = recipe;
        _text.text = recipe.recipeName;
        ItemInfo[] arrIngrts = recipe.ingredients;
        
        for (int idx = 0; idx < _icons.Length; idx++)
        {
            if (idx < arrIngrts.Length)
            {
                _icons[idx].transform.parent.gameObject.SetActive(true);
                _icons[idx].sprite = arrIngrts[idx].Sprite;
            }
            else
            {
                _icons[idx].transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void Init()
    {
        _icons = new SpriteRenderer[MAX_INGREDIENT_ICONS];

        for (int i = 0; i < MAX_INGREDIENT_ICONS; i++)
        {
            var GO = Instantiate(_iconPrefab);
            GO.transform.SetParent(_recipeIconsContainer.transform,false);
            GO.SetActive(false);
            _icons[i] = GO.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
    }
}
