using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField]
    private HandActionId fastAction = default;

    [SerializeField]
    private HandActionId[] slowActions = default;

    public HandActionId[] GetPossibleActions(GameObject actorGameObject)
    {
        if (slowActions.Length > 0) return slowActions;

        HandActionId[] x = { fastAction };
        return x;
    }

    public void TriggeredBy(GameObject actorGameObject)
    {
        TriggeredBy(fastAction, actorGameObject);
    }

    public void TriggeredBy(HandActionId action, GameObject actorGameObject)
    {
        PlayerActionsManager actorActionsManager = actorGameObject.GetComponent<PlayerActionsManager>();

        PlayerHandAction playerHandAction = PlayerHandAction.CreateAction(action, actorGameObject, gameObject);

        actorActionsManager.EnqueueAction(playerHandAction);
    }
}
