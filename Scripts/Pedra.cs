using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedra : MonoBehaviour
{
    public int qtd_N;
    public int qtd_P;
    public int qtd_K;
    public int health;
    public Item1 nitrogenio;
    public Item1 fosforo;
    public Item1 potassio;
    private bool isPlayerInRange = false;
    // Start is called before the first frame update
    public Pedra(int qtd_N, int qtd_P, int qtd_K)
    {
        this.qtd_N = qtd_N;
        this.qtd_P = qtd_P;
        this.qtd_K = qtd_K;
    }

    void Update()
    {
        if(isPlayerInRange && Input.GetButtonDown("Fire1"))
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
            for(int i=0; i<this.qtd_N; i++){
                DropItems(nitrogenio);
            }
            for(int i=0; i<this.qtd_P; i++){
                DropItems(fosforo);
            }
            for(int i=0; i<this.qtd_K; i++){
                DropItems(potassio);
            }
            
            Destroy(gameObject); // Remove a árvore
        }
    }

    void DropItems(Item1 item)
    {
        Transform player = GameObject.Find("Human").transform;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(0.1f, 0.5f);
        Vector2 spawnPosition = (Vector2)player.position + randomDirection * randomDistance;

        GameObject dropItemPrefab = Instantiate(item.prefab, spawnPosition, Quaternion.identity); //quaternion é a rotação, identity, seria estático
        dropItemPrefab.GetComponent<SpriteRenderer>().sprite = item.sprite;
        dropItemPrefab.GetComponent<PickUpItem>().item = item;
        //Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
    }

}
