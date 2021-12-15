using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoro_GIF : MonoBehaviour
{
    public Sprite[] Zoro_Images;
    public Image BG;
    
    
    void Update()
    {
        BG.sprite = Zoro_Images[(int)(Time.time*20)%6];   
    }
}
