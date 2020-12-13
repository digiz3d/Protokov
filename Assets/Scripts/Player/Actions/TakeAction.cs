using UnityEngine;
using System.Collections;

public class TakeAction : PlayerHandAction
{
    public override float Duration { get; set; } = 3f;

    public new bool RequiresRightHand = false;

    public TakeAction(GameObject interactorGameObject, GameObject interactedGameObject) : base(HandActionId.Take, interactorGameObject, interactedGameObject) { }

    public override IEnumerator GetBehaviour()
    {
        OnStart();
        PlayerInventory inventory = InteractorGameObject.GetComponent<PlayerInventory>();
        InventoryItem inventoryItem = InteractedGameObject.GetComponent<InventoryItem>();
        if (inventory != null && inventoryItem != null)
        {
            if (inventory.TryTake(inventoryItem))
            {
                //InteractedGameObject.SetActive(false);
                yield return new WaitForSeconds(Duration);
            }
        }
        OnEnd();
    }
}