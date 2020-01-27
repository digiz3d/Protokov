using UnityEngine;
using System.Collections;

public class PressButtonAction : PlayerHandAction
{
    public new float Duration = 1.5f;
    public new bool RequiresRightHand = false;

    public PressButtonAction(GameObject interactorGameObject, GameObject interactedGameObject) : base(HandActionId.PressButton, interactorGameObject, interactedGameObject) { }

    public override IEnumerator GetBehaviour()
    {
        OnStart();
        InteractorGameObject.GetComponent<PlayerController>().ControlsEnabled = false;
        Debug.Log("euhhh");

        Debug.Log("starting coroutine");
        yield return new WaitForSeconds(Duration);
        Debug.Log("After a few seconds");

        InteractorGameObject.GetComponent<PlayerController>().ControlsEnabled = true;
        Debug.Log("this is the child OnFinished");
        OnEnd();
    }
}