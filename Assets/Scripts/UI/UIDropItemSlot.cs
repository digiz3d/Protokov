using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDropItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData e)
    {
        Debug.Log($"GOOOOD OnDrop {e}");
    }
}
