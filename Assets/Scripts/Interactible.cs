using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField]
    private HandActionId action = default;

    public void TriggeredBy(GameObject actor)
    {
        PlayerActionsManager playerActionsManager = actor.GetComponent<PlayerActionsManager>();
        playerActionsManager.EnqueueAction(PlayerHandAction.CreateAction(action));
    }
}
