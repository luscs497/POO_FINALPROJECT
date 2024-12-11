using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    //cuida quando o item está no slot

    public InventoryItem myItem { get; set; } //item dentro desse slot

    public SlotTag myTag; //a tag desse slot
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(Inventory1.carriedItem == null)
            {
                return;
            }

            if (!(myTag == SlotTag.Holding && Inventory1.carriedItem.myItem.itemTag == SlotTag.Equipment) && !(myTag == SlotTag.Equipment && Inventory1.carriedItem.myItem.itemTag == SlotTag.Holding) && myTag != Inventory1.carriedItem.myItem.itemTag)
            {
                return;
            }
            
            SetItem(Inventory1.carriedItem);
        }
    }

    public void SetItem(InventoryItem item) //como atribuir um item para esse slot
    {
        if ((myTag == item.myItem.itemTag) || (myTag == SlotTag.Holding && (item.myItem.itemTag == SlotTag.Holding || item.myItem.itemTag == SlotTag.Equipment)) || (myTag == SlotTag.Equipment && (item.myItem.itemTag == SlotTag.Holding || item.myItem.itemTag == SlotTag.Equipment)))
        {
            item.myItem.itemTag = myTag; 

            Inventory1.carriedItem = null;

            if (item.activeSlot != null)
            {
                item.activeSlot.myItem = null;
            }

            myItem = item;

            myItem.activeSlot = this;
            myItem.transform.SetParent(transform);
            myItem.canvasGroup.blocksRaycasts = true;

            if (myTag != SlotTag.None)
            {
                Inventory1.Singleton.EquipEquipment(myTag, myItem);
            }
        }
    }
}
