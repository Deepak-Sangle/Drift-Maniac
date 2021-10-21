using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
	[Header("Game UI")]
	public RectTransform PauseMenu;
	public RectTransform ScoreMenu;

    private bool pauseUI = false;

    // Start is called before the first frame update
    void Start()
    {
        ScoreMenu.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {  
            if(pauseUI) {
                PlayResume();
                pauseUI = false;
            }
            else{
                pauseUI = true;     
                PlayPause();
            }
        }
    }

    public void PlayPause(){
        PauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void PlayResume(){
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
