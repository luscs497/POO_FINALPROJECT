using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public static InventoryItem carriedItem;
    [SerializeField] InventorySlot[] adding; //slots do craftings
    [SerializeField] InventorySlot result;
    [SerializeField] Transform draggablesTransform; //tudo que for arrastado ficará aqui
    [SerializeField] InventoryItem itemPrefab; //prefab do item que vamos criar
    [SerializeField] Item1[] items;
    public bool isCraftingOpen;
    public GameObject craftingInventoryCanvas;
    public static Crafting Singleton;

    void Awake()
    {
        Debug.Log("Entrou no craft manager");
        this.isCraftingOpen = false;
        Singleton = this;
        craftingInventoryCanvas.SetActive(false);
    }
    
    void Update()
    {
        //Debug.Log("Não está reconheceno");
        if (!this.isCraftingOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            Debug.Log("Abriu o Crafting");
            this.isCraftingOpen = true;
            craftingInventoryCanvas.SetActive(true);
        }else if(this.isCraftingOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            this.isCraftingOpen = false;
            Debug.Log("Fechou o Crafting");
            craftingInventoryCanvas.SetActive(false);
        }
        if(carriedItem == null)
        {
            return; //se não houver nenhum item aqui, sendo carregado, retorna nada;
        }

        carriedItem.transform.position = Input.mousePosition; //alterando a posição do item conforme a posição do mouse
    }

    public void SetCarriedItem(InventoryItem item){
        if(carriedItem != null)
        {
            if ((item.activeSlot.myTag == carriedItem.myItem.itemTag) || (item.activeSlot.myTag == SlotTag.Holding && (carriedItem.myItem.itemTag == SlotTag.Holding || carriedItem.myItem.itemTag == SlotTag.Equipment)) || (item.activeSlot.myTag == SlotTag.Equipment && (carriedItem.myItem.itemTag == SlotTag.Holding || carriedItem.myItem.itemTag == SlotTag.Equipment)))
            {
                carriedItem.myItem.itemTag = item.activeSlot.myTag; // Atualiza a tag do item
                item.activeSlot.SetItem(carriedItem);
            }
            else
            {
                return;
            }
            
        }
        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    public void SpawnInventoryItem(Item1 item = null)
    {
        Item1 _item = item;

        for (int i = 0; i < adding.Length; i++)
        {
            if(adding[i].myItem == null)
            {
                Instantiate(itemPrefab, adding[i].transform).Initialize(_item, adding[i]);
                break;
            }
        }
    }

    Item1 PickItem(Item1 pickItem) //ele vai pegar um item específico
    {
        Item1 selectedItem = null;

        foreach(var item in items) //ele verifica se o item escolhido existe na base de dados que contém todos os itens do jogo
        {
            if(item.name == pickItem.name) 
            {
                selectedItem = item;
                break;
            }
        }

        return selectedItem;
    }

    public void PickUpItem(Item1 item)
    {
        Item1 _item = item;
        if(_item == null)
        {
            _item = PickItem(item);
        }
        for (int i = 0; i < adding.Length; i++)
        {
            {
                if(adding[i].myItem == null)
                {
                    Instantiate(itemPrefab, adding[i].transform).Initialize(_item, adding[i]);
                    break;
                }
            }
        }
    }

    public void DropItem(InventoryItem item, bool evento = false)
    {        
        Destroy(item.gameObject);   
    }

    public bool getIsCraftingOpen()
    {
        return this.isCraftingOpen;
    }
}
