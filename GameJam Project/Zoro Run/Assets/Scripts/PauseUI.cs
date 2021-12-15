using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
	[Header("Game UI")]
	public RectTransform PauseMenu;
	public RectTransform ScoreMenu;
	public RectTransform RetryMenu;
	public RectTransform SettingsMenu;
	public RectTransform SettingsMenu2;
    private bool pauseUI = false;
    [HideInInspector] public bool isPause;
    void Start()
    {
        ScoreMenu.gameObject.SetActive(true);
        isPause = false;
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Escape)) {  
        //     if(pauseUI) {
        //         PlayResume();
        //         pauseUI = false;
        //     }
        //     else{
        //         pauseUI = true;     
        //         PlayPause();
        //     }
        // }
    }

    public void PlayPause(){
        SetCanvas(true, true, false, false, false, false);
        // isPause = true;
        Time.timeScale = 0.0f;
    }

    public void PlayResume(){
        SetCanvas(false, true, false, false, false, false);
        isPause = false;
        Time.timeScale = 1.0f;
    }

    public void PlayQuit(){
        Application.Quit();
    }

    public void PlaySettings(){
        SetCanvas(false, true, true, false, false, true);
    }
    public void PlaySettings2(){
        SetCanvas(false, false, false, true, false, true);
    }

    public void PlayRetry1(){
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void PlayBack1(){
        SetCanvas(true, true, false, false, false, false);
    }

    public void PlayBack2(){
        SetCanvas(false, false, false, false, true, true );        
    }

    public void SetCanvas(bool pause, bool score, bool set1, bool set2, bool retry, bool img){
        PauseMenu.gameObject.SetActive(pause);
        ScoreMenu.gameObject.SetActive(score);
        SettingsMenu.gameObject.SetActive(set1);
        SettingsMenu2.gameObject.SetActive(set2);
        RetryMenu.gameObject.SetActive(retry);
        gameObject.transform.GetChild(0).gameObject.SetActive(img);        
    }
}
