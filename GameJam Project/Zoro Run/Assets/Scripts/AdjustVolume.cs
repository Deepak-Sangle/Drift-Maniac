using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustVolume : MonoBehaviour
{
    public Slider VolumeSlider;
    public AudioManager AudioScript;
    void Start()
    {
        AudioScript = FindObjectOfType<AudioManager>();
        if(!PlayerPrefs.HasKey("MasterVolume")){
            PlayerPrefs.SetFloat("MasterVolume", 1.0f);
            Load();
        }
        else{
            Load();
        }
    }

    public void ChangeVolume(){
        AudioScript.AdjustVolume("All", VolumeSlider.value);
        Save();
    }

    private void Save(){
        PlayerPrefs.SetFloat("MasterVolume", VolumeSlider.value);
    }

    private void Load(){
        VolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }

}
