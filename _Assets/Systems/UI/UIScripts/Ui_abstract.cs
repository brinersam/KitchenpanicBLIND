using UnityEngine;
public abstract class Abstract_UI : MonoBehaviour
{
    //[SerializeField] protected GameObject _uiGobj;
    //public GameObject UiGObj => _uiGobj;

    public virtual void Activate(Abstract_UI prevstate = null)
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
        var stateOldref = System_UI.state;
        gameObject.SetActive(false);
        System_UI.state = newState;
        System_UI.state.Activate(stateOldref);
    }
}