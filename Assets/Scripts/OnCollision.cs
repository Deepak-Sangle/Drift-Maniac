using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    public CharacterMovement script;
    private bool isRotate = false;
    private void OnCollisionEnter(Collision collision){
        if(collision.transform.tag == "Zoro Model"){
            return;
        }
        script.OnCharacterColliderHit(collision.collider);
        if(collision.gameObject.tag == "Falling-Obstructs"){
            isRotate = true;    
     
        }
    }
}
