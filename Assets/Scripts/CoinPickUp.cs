using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
   [SerializeField] AudioClip coinPickUpSXF;
   [SerializeField] int pointsToAdd = 100;

   bool wasCollected = false;
   private void OnTriggerEnter2D(Collider2D other) 
   {
    if(other.tag == "Player" && !wasCollected)
    {   
        wasCollected = true;
        FindObjectOfType<GameSession>().AddToScore(pointsToAdd);
        AudioSource.PlayClipAtPoint(coinPickUpSXF, Camera.main.transform.position);
        gameObject.SetActive(false);
        Destroy(gameObject);
    } 
   }
}
