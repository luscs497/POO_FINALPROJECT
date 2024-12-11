using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mato : MonoBehaviour
{
    public int health = 1; // Vida do mato
    public Item1 milho;
    public Item1 cana;
    public Item1 cafe;
    public Item1 soja;
    private bool isPlayerInRange = false; // Verifica se o jogador está na área da árvore

    void Update()
    {
        // Verifica se o jogador está dentro do range da árvore e pressionou o botão de ataque
        if (isPlayerInRange && Input.GetButtonDown("Fire1"))
        {
            TakeDamage(1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o jogador entrou na área do trigger da árvore
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DropItems();
            Destroy(gameObject); // Remove a árvore
        }
    }
    void DropItems()
    {

        Transform player = GameObject.Find("Human").transform;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(0.1f, 0.5f);
        Vector2 spawnPosition = (Vector2)player.position + randomDirection * randomDistance;

        int milho_q = Random.Range(0, 3);
        for(int i=0; i<milho_q; i++){
            GameObject dropItemPrefab = Instantiate(milho.prefab, spawnPosition, Quaternion.identity);
            dropItemPrefab.GetComponent<SpriteRenderer>().sprite = milho.sprite;
            dropItemPrefab.GetComponent<PickUpItem>().item = milho;
        }


        int cana_q = Random.Range(0, 3);
        for(int i=0; i<cana_q; i++){
            GameObject dropItemPrefab = Instantiate(cana.prefab, spawnPosition, Quaternion.identity);
            dropItemPrefab.GetComponent<SpriteRenderer>().sprite = cana.sprite;
            dropItemPrefab.GetComponent<PickUpItem>().item = cana;
        }

        int cafe_q = Random.Range(0, 3);
        for(int i=0; i<cafe_q; i++){
            GameObject dropItemPrefab = Instantiate(cafe.prefab, spawnPosition, Quaternion.identity);
            dropItemPrefab.GetComponent<SpriteRenderer>().sprite = cafe.sprite;
            dropItemPrefab.GetComponent<PickUpItem>().item = cafe;
        }

        int soja_q = Random.Range(0, 3);
        for(int i=0; i<soja_q; i++){
            GameObject dropItemPrefab = Instantiate(soja.prefab, spawnPosition, Quaternion.identity);
            dropItemPrefab.GetComponent<SpriteRenderer>().sprite = soja.sprite;
            dropItemPrefab.GetComponent<PickUpItem>().item = soja;
        }
        
        //Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
    }
}
