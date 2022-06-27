using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightDialogue : MonoBehaviour
{
    public int id;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TextManager.myInstance.getTextFromFile(id);
        TextManager.myInstance.npcFaceIndex = id;
        TextManager.myInstance.myInputAble = true;
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        TextManager.myInstance.myInputAble = false;
        TextManager.myInstance.dialogueLong.gameObject.SetActive(false);
    }
}
