using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item1 item;
    bool alreadyPickup = false;

    public void OnTriggerStay2D(Collider2D collision) //quando o player chegar perto, ativa a condição
    {
        if(collision.transform.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E) && !alreadyPickup)
            {
                Inventory1.Singleton.PickUpItem(item);
                alreadyPickup = true;

                Destroy(this.gameObject);
            }
        }
    }
}
