using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField]
    private HandActionId action = default;

    public void TriggeredBy(GameObject actorGameObject)
    {
        PlayerActionsManager actorActionsManager = actorGameObject.GetComponent<PlayerActionsManager>();

        PlayerHandAction playerHandAction = PlayerHandAction.CreateAction(action, actorGameObject, gameObject);

        actorActionsManager.EnqueueAction(playerHandAction);
    }
}
