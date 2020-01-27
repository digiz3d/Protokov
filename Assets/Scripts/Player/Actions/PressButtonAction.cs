using UnityEngine;
using System.Collections;

public class PressButtonAction : PlayerHandAction
{
    public override float Duration { get; set; } = 1f;

    public new bool RequiresRightHand = false;

    public PressButtonAction(GameObject interactorGameObject, GameObject interactedGameObject) : base(HandActionId.PressButton, interactorGameObject, interactedGameObject) { }

    public override IEnumerator GetBehaviour()
    {
        OnStart();
        InteractorGameObject.GetComponent<PlayerController>().ControlsEnabled = false;

        yield return new WaitForSeconds(Duration);

        InteractorGameObject.GetComponent<PlayerController>().ControlsEnabled = true;
        OnEnd();
    }
}