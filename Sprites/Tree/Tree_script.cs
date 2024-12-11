using UnityEngine;

public class Tree_script : MonoBehaviour
{
    public int health = 5; // Vida da árvore
    public Item1 madeira;
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
            DropItems(madeira);
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
