using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[Header("Game UI")]
	public RectTransform MainMenu;
	public RectTransform OptionsMenu;
    // public SpawnEnvironment scrpt1;
    // public CameraMovement scrpt2;
    // public CharacterMovement movementScript;
    public GameObject go, gs;
    public bool isDied;
    private bool isPaused = false;
	// public RectTransform endPanel;

    public void SetCanvas(bool image, bool main, bool opt, bool gif, bool load){
        MainMenu.gameObject.SetActive(main);
        OptionsMenu.gameObject.SetActive(opt);
        gameObject.transform.GetChild(0).gameObject.SetActive(image);
        gameObject.transform.GetChild(3).gameObject.SetActive(gif);
        gameObject.transform.GetChild(4).gameObject.SetActive(load);
    }

    void Start()
    {
        // scrpt2 = FindObjectOfType<CameraMovement>();
        // movementScript = FindObjectOfType<CharacterMovement>();
        SetCanvas(true, true, false, true, false);

    }

    void Update()
    {

    }

    public void PlayGame(){
        SetCanvas(true, false, false, true, true);
        SceneManager.LoadScene(1);
    }

    public void PlayOption(){
        SetCanvas(true, false, true, true, false);
    }

    public void ChoseKakashi(){
        // go.SetActive(false);
        // gs.SetActive(true);
        // scrpt1.playerTransform = go.transform;
        // scrpt2.Target = go.transform;
    }

    public void ChoseZoro(){
        // go.SetActive(true);
        // gs.SetActive(false);
        // scrpt1.playerTransform = go.transform;
        // scrpt2.Target = go.transform;
    }

    public void PlayBack(){
        SetCanvas(true, true, false, true, false);
    }

    public void PlayDeath(){
        SetCanvas(true, false, false, true, false);
        
    }

    public void RetryGame(){
        PlayGame();
    }

    public void QuitGame(){
        Application.Quit();
    }
}