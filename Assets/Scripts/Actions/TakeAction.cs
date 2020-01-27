using UnityEngine;
using System.Collections;

public class TakeAction : PlayerHandAction
{
    public new float Duration = 5f;
    public new bool RequiresRightHand = false;

    public TakeAction(GameObject interactorGameObject, GameObject interactedGameObject) : base(HandActionId.Take, interactorGameObject, interactedGameObject) { }

    public override IEnumerator GetBehaviour()
    {
        OnStart();
        InteractedGameObject.SetActive(false);
        yield return new WaitForSeconds(Duration);
        OnEnd();
    }
}