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
    }

    protected void OnStart()
    {
        Debug.Log("PlayerHandAction::OnStart");
    }

    protected void OnEnd()
    {
        Debug.Log("PlayerHandAction::OnEnd");
        InteractorGameObject.GetComponent<PlayerActionsManager>().ActionFinished();
    }

    abstract public IEnumerator GetBehaviour();
}


