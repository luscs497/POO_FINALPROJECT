using System.Collections.Generic;
using UnityEngine;

public class ChestScript : Buildings
{
    public static InventoryItem carriedItem; //item que estamos arrastando no momento de gerenciamento do inventory
    public List<int> quantity = new List<int>();
    [SerializeField] InventorySlot[] inventorySlots; //slots do inventário
    [SerializeField] InventorySlot[] equipmentsSlots; //slots do equipamento do player
    [SerializeField] Transform draggablesTransform; //tudo que for arrastado ficará aqui
    [SerializeField] InventoryItem itemPrefab; //prefab do item que vamos criar
    [SerializeField] Item1[] items;
    [SerializeField] public List<string> inventoryItems = new List<string>();
    [SerializeField] public List<string> equipmentItems = new List<string>();
    public GameObject chestInventoryCanvas;
    public static ChestScript Singleton;
    public bool isChestOpen = false;

    void Awake()
    {
        Singleton = this;
        chestInventoryCanvas.SetActive(false);
    }
    
    void Update()
    {
        if(GetIsPlayerInRange())
        {
            InteractWith();
        }
        //SetIsPlayerInRange(false);
        
        if(carriedItem == null)
        {
            return; //se não houver nenhum item aqui, sendo carregado, retorna nada;
        }

        carriedItem.transform.position = Input.mousePosition; //alterando a posição do item conforme a posição do mouse
    }

    public override void InteractWith(){
        if (Input.GetKeyDown(KeyCode.Q))
        {
            base.DestroyObject(gameObject);
        }else if (!isChestOpen && Input.GetKeyDown(KeyCode.C))
        {
            OpenChest();
        }else if (isChestOpen && Input.GetKeyDown(KeyCode.C)){
            CloseChest();
        }

        //SetIsPlayerInRange(false);
    }

    public void OpenChest()
    {
        isChestOpen = true;
        chestInventoryCanvas.SetActive(true);
    }

    public void CloseChest()
    {
        isChestOpen = false;
        chestInventoryCanvas.SetActive(false);
    }

    public void RemoveItemFromChest(InventoryItem item)
    {
        int index = inventoryItems.IndexOf(item.myItem.itemName);
        if (index != -1)
        {
            quantity[index]--;
            if (quantity[index] <= 0)
            {
                inventoryItems.RemoveAt(index);
                quantity.RemoveAt(index);
            }
        }
        Destroy(item.gameObject);
    }

    public bool IsChestOpen()
    {
        return isChestOpen;
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

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
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
        if(_item.itemTag == SlotTag.None && !(inventoryItems.Contains(_item.itemName))){
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if(inventorySlots[i].myItem == null)
                {
                    Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
                    quantity.Add(1);
                    inventoryItems.Add(_item.itemName);
                    break;
                }
            }
        }else if(!(inventoryItems.Contains(_item.itemName))){
            for (int i = 0; i < equipmentsSlots.Length; i++)
            {
                if(equipmentsSlots[i].myItem == null)
                {
                    Instantiate(itemPrefab, equipmentsSlots[i].transform).Initialize(_item, equipmentsSlots[i]);
                    equipmentItems.Add(_item.itemName);
                    break;
                }
            }
        }else{
            setItemQuantity(_item.itemName, 1);
        }
        
    }

    public int getItemQuantity(string itemName)
    {
        int aux = buscarIndice(itemName);
        if(aux != -1){
            return quantity[aux];
        }
        return 0; // Retorna 0 se o item não for encontrado
    }

    public void setItemQuantity(string itemName, int x=0)
    {
        int aux = buscarIndice(itemName);
        if(aux != -1){
            quantity[aux] = getItemQuantity(itemName) + x;
        }
    }

    public void DropItem(InventoryItem item, bool evento = false)
    {
        if(!evento){
            Debug.Log("Drop item");
            SpawnObjectNearPlayer(item);
        }
        int indice = buscarIndice(item.myItem.itemName);
        if(item.myItem.itemTag != SlotTag.None){
            equipmentItems.RemoveAt(indice);
        }else{
            inventoryItems.RemoveAt(indice);
            quantity.RemoveAt(indice);
        }
        
        Destroy(item.gameObject);
        
    }

    public int buscarIndice(string itemName){
        int cont = 0;
        foreach (string item in inventoryItems)
        {
            if (item == itemName)
            {
                return cont;
            }
            cont++;
        }
        return -1;
    }

    public void SpawnObjectNearPlayer(InventoryItem item)
    {
        Transform player = GameObject.Find("Human").transform;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(0.1f, 0.5f);
        Vector2 spawnPosition = (Vector2)player.position + randomDirection * randomDistance;

        GameObject dropItemPrefab = Instantiate(item.myItem.prefab, spawnPosition, Quaternion.identity); //quaternion é a rotação, identity, seria estático
        dropItemPrefab.GetComponent<SpriteRenderer>().sprite = item.myItem.sprite;
        dropItemPrefab.GetComponent<PickUpItem>().item = item.myItem;
    }

    public List<string> getInventoryItems(){
        return this.inventoryItems;
    }

    public void setInventoryItems(List<string> newInventoryItems){
        this.inventoryItems = newInventoryItems;
    }

    public bool getUsing(string name){
        if(equipmentItems.Contains(name)){
            for (int i = 0; i < equipmentsSlots.Length; i++)
            {
                if(equipmentsSlots[i].myItem.myItem.itemName == name && equipmentsSlots[i].myItem.myItem.itemTag == SlotTag.Holding)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
