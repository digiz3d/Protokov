using UnityEngine;
using System;
using System.Collections;

[Serializable]
public enum HandActionId
{
    Attach,
    OpenDoor,
    PressButton,
    Take,
}

abstract public class PlayerHandAction
{
    private HandActionId id;

    protected float Duration = 0f;

    public GameObject InteractorGameObject { get; set; }
    public GameObject InteractedGameObject { get; set; }

    public Action OnStart { get; set; } = () => { };
    public Action OnFinished { get; set; } = () => { };

    public bool RequiresLeftHand { get; set; } = true;
    public bool RequiresRightHand { get; set; } = true;

    public static PlayerHandAction CreateAction(HandActionId id, GameObject interactorGameObject, GameObject interactedGameObject)
    {
        PlayerHandAction action = null;
        switch (id)
        {
            case HandActionId.Attach:
                break;


            case HandActionId.OpenDoor:
                break;

            case HandActionId.PressButton:
                action = new PressButtonAction(interactorGameObject, interactedGameObject);
                break;

            case HandActionId.Take:
                action = new TakeAction(interactorGameObject, interactedGameObject);
                break;

            default:
                break;
        }

        return action;
    }

    public PlayerHandAction(HandActionId id, GameObject interactorGameObject, GameObject interactedGameObject)
    {
        this.id = id;
        InteractorGameObject = interactorGameObject;
        InteractedGameObject = interactedGameObject;

        OnStart = () =>
        {
            Debug.Log("Starting action type " + id);
        };

        OnFinished = () =>
        {
            Debug.Log("Ending action type " + id);
            InteractorGameObject.GetComponent<PlayerActionsManager>().ActionFinished();
        };
    }

    abstract public IEnumerator GetBehaviour();
}

public class PressButtonAction : PlayerHandAction
{
    public new float Duration = 1.5f;
    public new bool RequiresRightHand = false;

    public PressButtonAction(GameObject interactorGameObject, GameObject interactedGameObject) : base(HandActionId.PressButton, interactorGameObject, interactedGameObject)
    {
        OnStart = () =>
        {
            InteractorGameObject.GetComponent<PlayerController>().ControlsEnabled = false;
            Debug.Log("euhhh");
        };

        Action originalOnFinishedAction = base.OnFinished;

        OnFinished = () =>
        {
            originalOnFinishedAction();
            InteractorGameObject.GetComponent<PlayerController>().ControlsEnabled = true;
            Debug.Log("this is the child OnFinished");
        };
    }

    public override IEnumerator GetBehaviour()
    {
        OnStart();
        Debug.Log("starting coroutine");
        yield return new WaitForSeconds(1f);
        Debug.Log("After a few seconds");
        OnFinished();
    }
}

public class TakeAction : PlayerHandAction
{
    public new float Duration = 1f;
    public new bool RequiresRightHand = false;

    public TakeAction(GameObject interactorGameObject, GameObject interactedGameObject) : base(HandActionId.Take, interactorGameObject, interactedGameObject) { }

    public override IEnumerator GetBehaviour()
    {
        OnStart();
        InteractedGameObject.SetActive(false);
        yield return 0;
        OnFinished();
    }
}