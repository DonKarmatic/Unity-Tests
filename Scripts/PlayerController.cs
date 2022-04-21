using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;

    private float moveSpeed;
    private float jumpForce;
    private bool isMoving;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        moveSpeed = 0.5f;
        jumpForce = 8f;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        animator.SetFloat("Height", moveVertical);

        if(moveHorizontal > 0 && !facingRight)
        {
          Flip();
        }
        else if(moveHorizontal < 0 && facingRight)
        {
          Flip();
        }
    }

    void FixedUpdate()
    {
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
        if(!isJumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "Platform")
      {
          isJumping = false;
      }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "Platform")
      {
          isJumping = true;
      }
    }

    void Flip()
    {
      facingRight = !facingRight;

      Vector2 currentScale = transform.localScale;
      currentScale.x *= -1;
      transform.localScale = currentScale;
    }
}
