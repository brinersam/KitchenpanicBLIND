using UnityEngine;
public abstract class Abstract_UI : MonoBehaviour
{
    //[SerializeField] protected GameObject _uiGobj;
    //public GameObject UiGObj => _uiGobj;

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnMenuBtn()
    {
        return;
    }

    public virtual void OnPause()
    {
        return;
    }

    public virtual void OnUnPause()
    {
        return;
    }
    public virtual void OnGameOver()
    {
        return;
    }

    public virtual void RefreshQueue()
    {
        return;
    }

    public virtual void SetStateTo(Abstract_UI newState)
    {
        gameObject.SetActive(false);
        System_UI.state = newState;
        System_UI.state.Activate();
    }
}