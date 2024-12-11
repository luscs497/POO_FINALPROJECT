using UnityEngine;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour
{
    public GameObject housePrefab; // Prefab da casa
    public GameObject chestPrefab; // Prefab do armazém
    public GameObject livePlantationPrefab; // Prefab da plantação
    [SerializeField] public GameObject deathPlantationPrefab;
    private GameObject currentBuilding; // O objeto atualmente sendo construído
    private bool isBuilding = false; // Verifica se o jogador está no modo de construção

    void Update(){
        //Debug.Log(itens);
        if (Inventory1.Singleton.inventoryItems.Contains("madeira"))
        {
            // Verifica se o jogador quer construir uma casa
            if (Input.GetKeyDown(KeyCode.H))
            {
                StartBuilding(GetHousePrefab());

            }

                // Verifica se o jogador quer construir um armazém
            if (Input.GetKeyDown(KeyCode.B))
            {
                StartBuilding(GetChestPrefab());
                //if(inventory.getItemQuantity(madeira.getItemName()) > 50){
                    //inventory.setItemQuantity("madeira", -50);
                //}
            }

                // Verifica se o jogador quer construir uma plantação
            if (Input.GetKeyDown(KeyCode.F))
            {
                int randomInt = Random.Range(0, 2);
                if(randomInt == 1){
                    StartBuilding(livePlantationPrefab);
                }else{
                    StartBuilding(deathPlantationPrefab);
                }
                
            }
        }

        // Se o jogador estiver no modo de construção, move o objeto
        if (isBuilding && currentBuilding != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Pega a posição do mouse no mundo 2D
            currentBuilding.transform.position = mousePosition;

            // Quando o jogador clicar com o botão esquerdo do mouse, coloca a construção
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding();
            }
        }
    }

    public void StartBuilding(GameObject buildingPrefab)
    {
        if (currentBuilding != null) Destroy(currentBuilding); // Remove qualquer construção em andamento anterior
        currentBuilding = Instantiate(buildingPrefab); // Cria a nova construção
        currentBuilding.GetComponent<Collider2D>().enabled = false; // Desativa colisão até ser colocada
        isBuilding = true; // Ativa o modo de construção
    }
    
    public void PlaceBuilding()
    {
        currentBuilding.GetComponent<Collider2D>().enabled = true; // Ativa a colisão da construção
        currentBuilding = null; // Limpa a referência para a construção atual
        isBuilding = false; // Sai do modo de construção
    }

    public GameObject GetHousePrefab(){
        return this.housePrefab;
    }

    public void SetHousePrefab(GameObject newHousePrefab){
        this.housePrefab = newHousePrefab;
    }

    public GameObject GetChestPrefab(){
        return this.chestPrefab;
    }

    public void SetChestPrefab(GameObject newChestPrefab){
        this.chestPrefab = newChestPrefab;
    }

    public GameObject GetCurrentBuildingPrefab(){
        return this.currentBuilding;
    }

    public void SetCurrentBuildingPrefab(GameObject newCurrentBuilding){
        this.currentBuilding = newCurrentBuilding;
    }

    public bool GetIsBuilding(){
        return this.isBuilding;
    }

    public void SetIsBuilding(bool newIsBuilding){
        this.isBuilding = newIsBuilding;
    }

    //public Item GetItem(){
        //return this.item;
    //}

    //public void SetItem(Item newItem){
        //this.item = newItem;
    //}

    //public Inventory GetInventory(){
        //return this.inventory;
    //}

    //public void SetInventory(Inventory newInventory){
        //this.inventory = newInventory;
    //}


}
