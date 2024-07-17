using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 4f;
    [SerializeField] Vector2 deathKcik = new Vector2(1f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform Gun;

    Vector2 moveInput;
    Rigidbody2D playerBody;
    Animator playerAnimantor;
    CapsuleCollider2D playerCollider;
    BoxCollider2D playerFeet;
    float gravityScaleAtStart;
    bool isAlive = true;


    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimantor = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = playerBody.gravityScale;

    }

    
    void Update()
    {
        if(!isAlive)
        {         
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        
    }
    void OnFire(InputValue value)
    {
        if(!isAlive)
        {
            return;
        }
        Instantiate(bullet, Gun.position, transform.rotation);

    }
    void OnMove(InputValue value)
    {
        if(!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();    
    }
    void OnJump(InputValue value)
    {
        if(!isAlive)
        {
            return;
        }
        if(!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground", "Ladders")))
        {
            return;
        }
        if(value.isPressed)
        {
            playerBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerBody.velocity.y);
        playerBody.velocity = playerVelocity;

        bool runing = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;   
        playerAnimantor.SetBool("isRunning", runing);
    
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
             transform.localScale = new Vector2(Mathf.Sign(playerBody.velocity.x), 1f);
        }
       
    }
    void ClimbLadder()
    {
        if(!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            playerBody.gravityScale = gravityScaleAtStart;
            playerAnimantor.SetBool("isClimbing", false);
            return;
        }
        bool climbing = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;   
        Vector2 climbVelocity = new Vector2(playerBody.velocity.x, moveInput.y * climbSpeed);
        playerBody.velocity = climbVelocity;
        playerAnimantor.SetBool("isClimbing", climbing);
        playerBody.gravityScale = 0;
    }
    void Die()
    {
        if(playerBody.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            playerAnimantor.SetTrigger("Dying");
            playerBody.velocity = deathKcik;
            FindObjectOfType<GameSession>().ProccessPlayerDeath();
        }
    }
}
