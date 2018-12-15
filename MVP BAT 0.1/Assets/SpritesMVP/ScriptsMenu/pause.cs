using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {
    public static bool GameIsPaused = false, SkillsDel;
    public GameObject pauseMenuUI;
    private void Start()
    {
        Pause();
    }
    void Update () {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Audiomanagerscript.PlaySound("pause");
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
	}
   public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        if (!SkillsDel) SkillsDel = true;
    }
   public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
}
