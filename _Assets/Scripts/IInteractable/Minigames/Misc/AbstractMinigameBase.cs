using System;
using UnityEngine;

public abstract class MinigameBase : MonoBehaviour
{
    public enum MinigameStateEnum
    {
        Offline = 0,
        Ongoing = 1
    }

    [SerializeField] protected ProgressBar progressBar;
    private Action<bool> stateFunc;

    protected IMinigameSubscriber caller;
    private MinigameStateEnum curState;

    public MinigameStateEnum CurState 
    {   get { return curState; }
        protected set { curState = ChangeState(value); }  
    }

    public void Initialize(IMinigameSubscriber caller)
    {
        this.caller = caller;
        curState = MinigameStateEnum.Offline;
    }

    public void Interact(bool alt = false)
    {
        stateFunc(alt);
    }

    public virtual void StartMinigame()
    {
        CurState = MinigameStateEnum.Ongoing;
    }
    public virtual void InterruptMinigame()
    {
        CurState = MinigameStateEnum.Offline;
    }
    public virtual void FinishMinigame()
    {
        InterruptMinigame();
        caller.OnMinigameFinished();
    }

    protected virtual void Interact_Offline(bool alt)
    {
        return;
    }
    protected virtual void Interact_Ongoing(bool alt)
    {
        return;
    }
    
    private MinigameStateEnum ChangeState(MinigameStateEnum value)
    {
        switch (value)
        {
            case MinigameStateEnum.Offline:{
                stateFunc = Interact_Offline;
                progressBar.gameObject.SetActive(false);
                return MinigameStateEnum.Offline;
            }
            case MinigameStateEnum.Ongoing:{
                stateFunc = Interact_Ongoing;
                progressBar.gameObject.SetActive(true);
                return MinigameStateEnum.Ongoing;
            }
        }
        Debug.LogError("UNHANDLED STATE @ minigame");
        return 0;
    }
}