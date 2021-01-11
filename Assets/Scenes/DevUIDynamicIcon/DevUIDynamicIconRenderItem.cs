using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevUIDynamicIcon
{
    public class DevUIDynamicIconRenderItem : MonoBehaviour
    {
        public InventoryItem itemToRender;
        public UIInventoryDraggableItem draggableItem;

        // Start is called before the first frame update
        void Start()
        {
            draggableItem.Setup(GetComponent<Canvas>(), itemToRender);
        }

    }
}