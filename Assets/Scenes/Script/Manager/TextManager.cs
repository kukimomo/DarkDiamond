using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [Header("普通显示对话框")] 
    public Text str;
    public Image dialogueSimple;

    [Header("长文本对话框")] 
    private bool inputable;

    private bool textFinished;
    public float textSpeed;
    public Image dialogueLong;
   
    public Image face;
    public Text longText;
    
    public TextAsset[] TextAssets;
   
    public List<string> textList = new List<string>();
    [SerializeField]
    private int textRow;

    [Header("头像信息")] 
    public Sprite[] dialogueTypeSprites;//对话框类型   0的话表示是普通对话框         1的话表示长文本对话框
    public Sprite player;
    public Sprite[] npcFaces;//对话框玩家的头像
    public int npcFaceIndex;
    private static TextManager instance;

    public static TextManager myInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TextManager>();
            }

            return instance;
        }
    }

    private bool locked = false;
    void Start()
    {
        
    }

    public bool myInputAble
    {
        set
        {
            inputable = value;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
      
        if (inputable)
        {
           
                if (Input.GetKeyDown(KeyCode.R) && textRow == textList.Count-1)
                {
               
                    dialogueLong.gameObject.SetActive(false);
                    textRow = 0;
                    return;
                }
                if ((Input.GetKeyDown(KeyCode.R)) && textFinished) 
                {

                    dialogueLong.gameObject.SetActive(true);
                
                    StartCoroutine(showTextContent());
                }
                
        }
        
    }

    public void getTextFromFile(int val)
    {
        textList.Clear();
        textRow = 0;
        string[] contens = TextAssets[val].text.Split('\n');
        foreach (string content in contens)
        {
            textList.Add(content);
        }
        dialogueLong.sprite = dialogueTypeSprites[1];//这一段必然是长文本对话框
        textFinished = true;
    }

    IEnumerator showTextContent()
    {
  
        textFinished = false;
        longText.text = "";
        switch (textList[textRow])
        {
            case "NPC\r":
                face.sprite = npcFaces[npcFaceIndex];
                textRow++;
                break;
            case "Player\r" :
                face.sprite = player;
                textRow++;
                break;
        }

        for (int i = 0; i < textList[textRow].Length; i++)
        {
            longText.text += textList[textRow][i];
            yield return new WaitForSeconds(textSpeed);
        }

        textFinished = true;
        textRow++;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    //简单的文本显示类型
    public void setSimpleDialogue(string Text)
    {
        dialogueSimple.gameObject.SetActive(true);
        dialogueSimple.sprite = dialogueTypeSprites[0];
        str.text = Text;
    }

    public void closeDialogue()
    {
        dialogueSimple.gameObject.SetActive(false);
        str.text = "";
    }
}
