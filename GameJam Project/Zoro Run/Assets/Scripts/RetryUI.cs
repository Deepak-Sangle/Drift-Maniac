using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryUI : MonoBehaviour
{
	[Header("Game UI")]
	public RectTransform PauseMenu;
	public RectTransform RetryMenu;
	public RectTransform ScoreMenu;

    public void PlayRetry(){
        PauseMenu.gameObject.SetActive(false);
        RetryMenu.gameObject.SetActive(true);
        ScoreMenu.gameObject.SetActive(false);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().PlaySound("Naruto-Main Menu");
    }
    
    public void RetryScene(){
        SceneManager.LoadScene(1);
    }

    public void BacktoMain(){
        SceneManager.LoadScene(0);
    }
    
}
