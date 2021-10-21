using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[Header("Game UI")]
	public RectTransform MainMenu;
	public RectTransform OptionsMenu;
	public RectTransform PauseMenu;
	public RectTransform RetryMenu;
	public RectTransform ScoreMenu;
    public SpawnEnvironment scrpt1;
    public CameraMovement scrpt2;
    public CharacterMovement movementScript;
    public GameObject go, gs;

    private bool isPaused = false;
	// public RectTransform endPanel;

    void Start()
    {
        scrpt2 = FindObjectOfType<CameraMovement>();
        movementScript = FindObjectOfType<CharacterMovement>();
        MainMenu.gameObject.SetActive(true);
        OptionsMenu.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        ScoreMenu.gameObject.SetActive(false);
        RetryMenu.gameObject.SetActive(false);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(6).gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {  
            if(isPaused) {
                PlayResume();
                isPaused = false;
            }
            else{
                isPaused = true;     
                PlayPause();
            }
        }
        if(SceneManager.GetActiveScene().name == "GameScene" && movementScript.isDeath) PlayDeath();
    }

    public void PlayGame(){
        SceneManager.LoadScene(1);

        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(6).gameObject.SetActive(true);
        OptionsMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        ScoreMenu.gameObject.SetActive(false);
        RetryMenu.gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().StopSound("Naruto-Main Menu");
        FindObjectOfType<AudioManager>().PlaySound("MainTheme");
    }

    public void PlayOption(){
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);
        RetryMenu.gameObject.SetActive(false);
        ScoreMenu.gameObject.SetActive(false);
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
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(true);
        OptionsMenu.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        RetryMenu.gameObject.SetActive(false);
        ScoreMenu.gameObject.SetActive(false);
    }

    public void PlayPause(){
        Time.timeScale = 0.0f;
        OptionsMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);
        RetryMenu.gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().PlaySound("Naruto-Main Menu");
        FindObjectOfType<AudioManager>().StopSound("MainTheme");
    }

    public void PlayResume(){
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        RetryMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        FindObjectOfType<AudioManager>().StopSound("Naruto-Main Menu");
        FindObjectOfType<AudioManager>().PlaySound("MainTheme");
    }

    public void PlayDeath(){
        StartCoroutine(Die());
    }

    public IEnumerator Die(){
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        RetryMenu.gameObject.SetActive(true);
        ScoreMenu.gameObject.SetActive(true);
    }

    public void RetryGame(){
        PlayGame();
    }

    public void QuitGame(){
        Application.Quit();
    }
}