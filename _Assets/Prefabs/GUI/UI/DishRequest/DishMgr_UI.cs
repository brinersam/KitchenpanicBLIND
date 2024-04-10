using UnityEngine;
using DG.Tweening;

public class DishMgr_UI : MonoBehaviour
{
    private DishRequestBox[] _recipeBoxes;
    private int _lastIdx = 0;
    [SerializeField] private GameObject recipeBoxPrefab;
    [SerializeField] private TimerElementUI TimerUI;

    private void Awake()
    {
        Init();
    }

    private void Start() 
    {
        System_DishMgr.OnRecipeAdded += Recipe_Add;
        System_DishMgr.OnRecipeRemoved += Recipe_Remove;
        System_Tick.OnTick += UpdateClockVisual;
    }

    public void UpdateClockVisual()
    {
        TimerUI.SetVisual((float)System_Session.TimeCur/System_Session.TimeMax);
    }

    private void Recipe_Add(RecipeSO recipe)
    {
        if (_lastIdx >= _recipeBoxes.Length)
        {
            Debug.LogWarning("Recipe list overflow prevented");
            return;
        }

        _recipeBoxes[_lastIdx].gameObject.SetActive(true);
        _recipeBoxes[_lastIdx].UpdateVisuals(recipe);

        _lastIdx++;
    }

    private void Recipe_Remove(RecipeSO recipe)
    {
        if (_lastIdx - 1 < 0)
        {
            Debug.LogWarning("Removal from empty recipe list prevented");
            return;
        }

        int j;
        for (j = 0; j < _recipeBoxes.Length; j++)
        {
            if (_recipeBoxes[j].LastData == recipe)
                break;
        }

        for (int i = j; i < -1 + _recipeBoxes.Length; i++)
        {
            if (_recipeBoxes[i].gameObject.activeInHierarchy == false)
                break;

            _recipeBoxes[i].UpdateVisuals(_recipeBoxes[i+1]);
            Animate_ShuffleUp(i);
        }

        _lastIdx--;
        _recipeBoxes[_lastIdx].gameObject.SetActive(false);
    }

    private void Animate_ShuffleUp(int idx)
    {
        Vector3 posOrig;
        Vector3 posOffset;

        posOrig = posOffset =  _recipeBoxes[idx].transform.position;
        
        posOffset.y -= 0.15f; // for some reason fucks with other 2
        posOffset.x = posOrig.x; 
        posOffset.z = posOrig.z;

        _recipeBoxes[idx].transform.position = posOffset;

        _recipeBoxes[idx].transform.DOShakePosition(0.2f);
        _recipeBoxes[idx].transform.DOMove(posOrig,0.2f);
    }

    private void Init()
    {
        _recipeBoxes = new DishRequestBox[System_DishMgr.MaximumDishes];
        for (int i = 0; i < _recipeBoxes.Length; i++)
        {
            var gObj = Instantiate(recipeBoxPrefab);
            gObj.transform.SetParent(transform,false);
            gObj.SetActive(false);
            _recipeBoxes[i] = gObj.GetComponent<DishRequestBox>();
        }
    }
}
