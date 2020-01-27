using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UILoadingAction))]
public class PlayerActionsManager : MonoBehaviour
{
    private Queue<PlayerHandAction> actionsQueue;

    private PlayerHandAction CurrentLeftHandAction = null;
    private PlayerHandAction CurrentRightHandAction = null;

    public bool IsLeftHandBusy { get; set; } = false;
    public bool IsRightHandBusy { get; set; } = false;

    private bool isExecutingAction = false;
    private Coroutine currentCoroutine;

    private UILoadingAction loadingAction;
    void Start()
    {
        actionsQueue = new Queue<PlayerHandAction>();
        loadingAction = GetComponent<UILoadingAction>();
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
        loadingAction.Show(action.Duration);
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
        loadingAction.Hide();
        currentCoroutine = null;
        isExecutingAction = false;
    }

}
