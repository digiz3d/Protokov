using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField]
    private HandActionId action = default;

    public void TriggeredBy(PlayerActionsManager actorActionsManager)
    {
        actorActionsManager.EnqueueAction(PlayerHandAction.CreateAction(action));
    }
}
