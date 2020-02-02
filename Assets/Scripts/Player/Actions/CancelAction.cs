using UnityEngine;
using System.Collections;

public class CancelAction : PlayerHandAction
{
    public override float Duration { get; set; } = 0f;

    public new bool RequiresRightHand = false;

    public CancelAction(GameObject interactorGameObject, GameObject interactedGameObject) : base(HandActionId.Cancel, interactorGameObject, interactedGameObject) { }

    public override IEnumerator GetBehaviour()
    {
        OnStart();
        yield return new WaitForSeconds(0);
        OnEnd();
    }
}