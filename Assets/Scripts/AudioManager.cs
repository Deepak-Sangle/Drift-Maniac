using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; 
    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.isLoop;
        }
        if(SceneManager.GetActiveScene().name == "GameScene") PlaySound("MainTheme");
        else PlaySound("Naruto-Main Menu");
    }

    public void PlaySound(string name){
        foreach(Sound s in sounds){
            if(s.name == name) 
                s.source.Play();
        }
    }

    public void StopSound(string name){
        foreach(Sound s in sounds){
            if(s.name == name)
                s.source.Stop();
        }
    }

}
