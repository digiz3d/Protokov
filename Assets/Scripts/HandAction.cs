using System;

[Serializable]
public enum HandActionId
{
    Attach,
    Grab,
    OpenDoor,
    PressButton,
}

public class PlayerHandAction
{
    private HandActionId id;

    public Action OnBeforeStart { get; set; } = () => { };
    public Action OnFinished { get; set; } = () => { };

    public bool RequiresLeftHand { get; set; } = true;
    public bool RequiresRightHand { get; set; } = true;

    public static PlayerHandAction CreateAction(HandActionId id)
    {
        PlayerHandAction action = new PlayerHandAction(id);

        switch (id)
        {
            case HandActionId.Attach:
                break;

            case HandActionId.Grab:
                break;

            case HandActionId.OpenDoor:
                action.RequiresRightHand = false;
                break;

            case HandActionId.PressButton:
                action.RequiresRightHand = false;
                break;

            default:
                break;
        }

        return action;
    }

    public PlayerHandAction(HandActionId id)
    {
        this.id = id;

        switch (id)
        {

        }
    }

    public PlayerHandAction(HandActionId id, bool requiresLeftHand, bool requiresRightHand)
    {
        this.id = id;
        RequiresLeftHand = requiresLeftHand;
        RequiresRightHand = requiresRightHand;
    }
}
