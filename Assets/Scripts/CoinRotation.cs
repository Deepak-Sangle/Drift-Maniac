using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float speed = 25f;
    public ScoreUI coincount;
    void Update()
    {
        transform.Rotate(0,speed*Time.deltaTime, 0);
        coincount = FindObjectOfType<ScoreUI>();
        
    }

    private void OnTriggerEnter(Collider other){
        coincount.numOfCoins += 1;
        // Debug.Log(CharacterMovement.numOfCoins);
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other){
        FindObjectOfType<AudioManager>().PlaySound("CoinsCatched");

    }
}
