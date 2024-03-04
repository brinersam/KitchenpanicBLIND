using System;
using System.Collections;
using UnityEngine;


public interface IMinigameSubscriber : IInteractable
{
    void OnMinigameFinished();
}