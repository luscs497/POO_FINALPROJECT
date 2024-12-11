using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    //Cuida do item em si
    private Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }
    public Item1 myItem { get; set; }
    public InventorySlot activeSlot { get; set; } //onde nosso item estará

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>(); //caso eu esqueça de arrastar o canvasgroup pra ele; este pega os componentes CanvasGroup daqui de dentro de atribui a nossa variavel canvasGroup
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(Item1 item, InventorySlot parent) //parent é o slot que o item está anexado
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
        itemIcon.sprite = item.sprite;
    } 

    public void OnPointerClick(PointerEventData eventData)
    {

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory1.Singleton.SetCarriedItem(this);
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(ChestScript.Singleton.isChestOpen){
                Inventory1.Singleton.DropItem(this, true);
                ChestScript.Singleton.PickUpItem(this.myItem);
            }else{
                Inventory1.Singleton.DropItem(this, false);
            }
        }
    }

    //public void setInventory(Inventory1 newInventory){
        //this.inventory = newInventory;
    //}
}



