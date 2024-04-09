using UnityEngine;
public abstract class Abstract_UI : MonoBehaviour
{
    [SerializeField] protected GameObject uiGobj;
    public GameObject UiGObj => uiGobj;

    public virtual void Activate()
    {
        uiGobj.SetActive(true);
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

    public virtual void RefreshQueue()
    {
        return;
    }

    public virtual void OnTick()
    {
        return;
    }
    
    public virtual void SetStateTo(Abstract_UI newState)
    {
        uiGobj.SetActive(false);
        System_UI.state = newState;
        System_UI.state.Activate();
    }
}