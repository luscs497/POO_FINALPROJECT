using UnityEngine;

public class DeathPlant : MonoBehaviour
{
    public Sprite deathSprite;
    private SpriteRenderer spriteRenderer;
    public bool isPlantInRange;
    public GameObject livePlant;
    int sickness = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plantation")){
            isPlantInRange = true;
        }else{
            isPlantInRange = false;
        }
    }

    void Update()
    {
        if(isPlantInRange)
        {
            Instantiate(livePlant, this.transform.position, this.transform.rotation);
            Inventory1.Singleton.money += 5;
            Destroy(this.gameObject);
        }
    }
    public int getSickness()
    {
        return this.sickness;
    }

    public void reAlive()
    {

    }

    //Implementar: sistema de colheita, sistema de adubar, de plantação somente com a semente
}

