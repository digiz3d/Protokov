using UnityEngine;

namespace DevUIDynamicIcon
{
    [RequireComponent(typeof(Canvas))]
    public class DevUIDynamicIconRenderItem : MonoBehaviour
    {
        public InventoryItem itemToRender;
        public UIInventoryDraggableItem draggableItem;

        // Start is called before the first frame update
        void Update()
        {
            draggableItem.Setup(GetComponent<Canvas>(), itemToRender);
        }

    }
}