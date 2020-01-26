using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerActionsManager : MonoBehaviour
{
    private Queue<PlayerHandAction> actionsQueue;

    private PlayerHandAction CurrentLeftHandAction = null;
    private PlayerHandAction CurrentRightHandAction = null;

    public bool IsLeftHandBusy { get; set; } = false;
    public bool IsRightHandBusy { get; set; } = false;

    private bool isExecutingAction = false;
    private Coroutine currentCoroutine;

    void Start()
    {
        actionsQueue = new Queue<PlayerHandAction>();
    }

    void Update()
    {
        if (actionsQueue.Count > 0)
        {
            PlayerHandAction nextAction = actionsQueue.Peek();
            if (CanExecuteAction(nextAction))
            {
                ExecuteAction(actionsQueue.Dequeue());
            }
        }
    }

    bool CanExecuteAction(PlayerHandAction action)
    {
        if (IsLeftHandBusy && action.RequiresLeftHand) return false;
        if (IsRightHandBusy && action.RequiresRightHand) return false;

        return true;
    }

    void ExecuteAction(PlayerHandAction action)
    {
        if (isExecutingAction || currentCoroutine != null) return;

        isExecutingAction = true;
        currentCoroutine = StartCoroutine(action.GetBehaviour());
    }

    public void EnqueueAction(PlayerHandAction action)
    {
        actionsQueue.Enqueue(action);
    }

    public void StopCurrentAction()
    {
        if (isExecutingAction)
        {
            StopCoroutine(currentCoroutine);
            isExecutingAction = false;
        }
    }

    public void ActionFinished()
    {
        currentCoroutine = null;
        isExecutingAction = false;
    }

}
