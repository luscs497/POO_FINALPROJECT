using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buildings : MonoBehaviour
{
    private bool isPlayerInRange;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetIsPlayerInRange(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetIsPlayerInRange(false);
        }
    }

    public bool GetIsPlayerInRange(){
        return this.isPlayerInRange;
    }

    public void SetIsPlayerInRange(bool newIsPlayerInRange){
        this.isPlayerInRange = newIsPlayerInRange;
    }

    public Sprite GetSprite(){
        return this.sprite;
    }

    public void SetSPrite(Sprite newSprite){
        this.sprite = newSprite;
    }

    public SpriteRenderer GetSpriteRenderer(){
        return this.spriteRenderer;
    }

    public void SetSpriteRenderer(SpriteRenderer newSpriteRenderer){
        this.spriteRenderer = newSpriteRenderer;
    }

    public void DestroyObject(GameObject gameObject){
        Destroy(gameObject);
    }

    public abstract void InteractWith();
    
}
