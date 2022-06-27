using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isStop;
    // Start is called before the first frame update
    public GameObject quitPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.myInstance.PlaySound("open");
            if (!isStop)
            {
                stop();
            }
            else
            {
                resume();
            }
        }
    }

    public void stop()
    {
        pauseMenu.SetActive(true);
        isStop = true;
        Time.timeScale = 0;
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        isStop = false;
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    
    //退出界面
    public void No()
    {
        AudioManager.myInstance.PlaySound("open");
        quitPanel.SetActive(false);
    }

    public void Yes()
    {
        AudioManager.myInstance.PlaySound("open");
        Application.Quit();
    }

    public void showQuitPanel()
    {
        quitPanel.SetActive(true);
    }
   
}
