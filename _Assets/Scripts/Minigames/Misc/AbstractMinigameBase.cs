using System;
using UnityEngine;

public abstract class MinigameBase: MonoBehaviour
{
    public enum MinigameStateEnum
    {
        Offline = 0,
        Ongoing = 1
    }

    [SerializeField] protected ProgressBar progressBar;
    [SerializeField] protected AudioSource audioSrc;
    
    private delegate void stateFuncDef (bool alt, out bool returnControl);
    private stateFuncDef stateFunc;

    protected IMinigameSubscriber caller;
    private MinigameStateEnum __curState;

    public MinigameStateEnum CurState 
    {   get { return __curState; }
        protected set { __curState = ChangeState(value); }  
    }

    public void Initialize(IMinigameSubscriber caller)
    {
        this.caller = caller;
        CurState = MinigameStateEnum.Offline; // if minigames break i changed __curState to this
    }

    public void Interact(out bool returnControl, bool alt = false)
    {
        stateFunc(alt, out bool control);
        returnControl = control;
    }

    public virtual void Minigame_Start()
    {
        CurState = MinigameStateEnum.Ongoing;
    }
    public virtual void Minigame_Interrupt()
    {
        CurState = MinigameStateEnum.Offline;
    }
    public virtual void Minigame_ForceFinish()
    {
        Minigame_Interrupt();
        caller.OnMinigameFinished();
    }

    protected virtual void Interact_Offline(bool alt, out bool rc)
    {
        rc = false;
        return;
    }
    protected virtual void Interact_Ongoing(bool alt, out bool rc)
    {
        rc = false;
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