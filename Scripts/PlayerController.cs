using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public float input_x = 0;
    public float input_y = 0;
    public float speed = 2.5f;
    bool isWalking = false;
    public DayNightCycle day;
    Rigidbody2D rb2D;
    Vector2 movement = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        isWalking = false;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);
        movement = new Vector2(input_x, input_y);
        if(isWalking)
        {
            playerAnimator.SetFloat("InputX", input_x);
            playerAnimator.SetFloat("InputY", input_y);
        }

        playerAnimator.SetBool("IsWalking", isWalking);

        if(Input.GetButtonDown("Fire1")){
            playerAnimator.SetTrigger("Attack");
        } 
    }

    private void FixedUpdate(){
        if(day.timeOfDay < 0.5){
            speed = 0.6f;
        }else{
            speed = 1.6f;
        }
        rb2D.MovePosition(rb2D.position + movement * speed * Time.fixedDeltaTime);
    }
}
