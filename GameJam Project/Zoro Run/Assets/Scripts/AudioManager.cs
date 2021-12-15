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
            s.source.volume = s.volume;
        }
        if(SceneManager.GetActiveScene().name == "GameScene") {
            PlaySound("MainTheme");
            StopSound("Naruto-Main Menu");
        }
        else {
            PlaySound("Naruto-Main Menu");
            StopSound("MainTheme");
        }
        if(PlayerPrefs.HasKey("MasterVolume")){
            AdjustVolume("All", PlayerPrefs.GetFloat("MasterVolume"));
        }
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

    public void AdjustVolume(string name, float val){
        foreach(Sound s in sounds){
            if(s.name == name || name=="All"){
                s.source.volume = s.volume*val;
            }
        }
    }

}
