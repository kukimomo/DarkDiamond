using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image helpImage;

    public Image infoImage;

    public Text infoText;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    void Start()
    {
        quitPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject quitPanel;
    private Coroutine cur;
    public void StartGame()
    {
        if (cur != null)
        {
            Debug.Log("暂停了");
            StopCoroutine(cur);
        }
        AudioManager.myInstance.PlaySound("click");
        SceneManager.LoadScene(1);
        
    }

   

    public void Help()
    {
        AudioManager.myInstance.PlaySound("click");
        helpImage.gameObject.SetActive(true);
    }

    public void closeHelp()
    {
        AudioManager.myInstance.PlaySound("click");
        helpImage.gameObject.SetActive(false); 
    }

    public void GameInfo()
    {
        Debug.Log("声音");
        AudioManager.myInstance.PlaySound("click");
        infoImage.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(showInfo());
    }

    public void CloseGameInfo()
    {
        AudioManager.myInstance.PlaySound("click");
        infoImage.gameObject.SetActive(false); 
        infoText.text = "";
        if (cur != null)
        {
            StopCoroutine(cur);
        }
    }

    IEnumerator showInfo()
    {
        string str1 =
            "主人公Jimmy有一天醒来发现村子一片寂静，窗外没有孩童欢快的嬉戏声也没有夏日蝉鸣声，她像往日一样推开大门，发现太阳暗红得可怕，似乎过度操劳，摇摇欲坠。主人公心脏怦怦急跳，瞳孔放大，豆大般的汗水从两颊流淌，突然一道强光直射，炫丽金蓝的火焰汇成一行晦涩难懂，奇形怪状的字体”Original”，主人公摸不着头脑，接着她的身体不受控制，一缕看不见的妖风将她卷走…";
        infoText.text = "";
        Debug.Log(str1.Length);
        for (int i = 0; i < str1.Length; i++)
        {
            Debug.Log("i==="+i+"  协程里");
            infoText.text += str1[i];
            yield return new WaitForSeconds(0.005f);
        }

        yield return null;
    }

    public void clickQuit()
    {
        AudioManager.myInstance.PlaySound("click");
        quitPanel.SetActive(true);
    }
    
    public void YesQuit()
    {
        AudioManager.myInstance.PlaySound("click");
        Application.Quit();
    }
    
    public void cancelQuit()
    {
        AudioManager.myInstance.PlaySound("click");
        quitPanel.SetActive(false);
    }
    
    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }
    
    
}
