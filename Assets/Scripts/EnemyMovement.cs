using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D enemyBody;
    BoxCollider2D enemyBox;

    
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemyBox = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        enemyBody.velocity = new Vector2 (moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }
    void FlipEnemyFacing()
    {
         transform.localScale = new Vector2(-(Mathf.Sign(enemyBody.velocity.x)), 1f);     
    }
    
}
