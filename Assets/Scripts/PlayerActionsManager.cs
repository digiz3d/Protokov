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

    // Start is called before the first frame update
    void Start()
    {
        actionsQueue = new Queue<PlayerHandAction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actionsQueue.Count > 0)
        {
            PlayerHandAction nextAction = actionsQueue.Peek();
            if (CanExecute(nextAction))
            {
                TryExecuteAction(actionsQueue.Dequeue());
            }
        }
    }

    bool CanExecute(PlayerHandAction action)
    {
        if (IsLeftHandBusy && action.RequiresLeftHand) return false;
        if (IsRightHandBusy && action.RequiresRightHand) return false;

        return true;
    }

    void TryExecuteAction(PlayerHandAction action)
    {
        //action.Execute(this);
    }

    public void EnqueueAction(PlayerHandAction action)
    {
        actionsQueue.Enqueue(action);
    }

}
