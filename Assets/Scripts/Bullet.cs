using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletBody;
    [SerializeField] float bulletSpeed = 1f;   
    [SerializeField] int pointsToAdd = 50;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        bulletBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        bulletBody.velocity = new Vector2(xSpeed,0f);
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            FindObjectOfType<GameSession>().AddToScore(pointsToAdd);
            Destroy(other.gameObject);           
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }

}
 