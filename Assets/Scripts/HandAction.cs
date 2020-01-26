using UnityEngine;
using System;
using System.Collections;

[Serializable]
public enum HandActionId
{
    Attach,
    Grab,
    OpenDoor,
    PressButton,
}

abstract public class PlayerHandAction
{
    private HandActionId id;

    public Action OnBeforeStart { get; set; } = () => { };
    public Action OnFinished { get; set; } = () => { };

    public bool RequiresLeftHand { get; set; } = true;
    public bool RequiresRightHand { get; set; } = true;

    public static PlayerHandAction CreateAction(HandActionId id)
    {
        PlayerHandAction action = null;

        switch (id)
        {
            case HandActionId.Attach:
                break;

            case HandActionId.Grab:
                break;

            case HandActionId.OpenDoor:
                break;

            case HandActionId.PressButton:
                action = new PressButtonAction();
                break;

            default:
                break;
        }

        return action;
    }

    public PlayerHandAction(HandActionId id)
    {
        this.id = id;
    }

    public PlayerHandAction(HandActionId id, bool requiresLeftHand, bool requiresRightHand)
    {
        this.id = id;
        RequiresLeftHand = requiresLeftHand;
        RequiresRightHand = requiresRightHand;
    }

    abstract public IEnumerator GetBehaviour();
}

public class PressButtonAction : PlayerHandAction
{
    public new bool RequiresRightHand = false;

    public PressButtonAction() : base(HandActionId.PressButton) { }

    public override IEnumerator GetBehaviour()
    {
        Debug.Log("starting coroutine");
        yield return new WaitForSeconds(1f);
        Debug.Log("After a few seconds");
    }
}