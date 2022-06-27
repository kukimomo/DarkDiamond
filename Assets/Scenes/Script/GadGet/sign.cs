using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class sign : MonoBehaviour
{
    public string str;
  
    
    private void Awake()
    {
    
    }

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
      
        if (other.CompareTag("Player"))
        {

            TextManager.myInstance.setSimpleDialogue(str);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TextManager.myInstance.closeDialogue();
    }
}
