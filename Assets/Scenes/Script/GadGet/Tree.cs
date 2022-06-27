using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject dialogue;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        dialogue = transform.GetChild(0).gameObject;
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
            anim.SetBool("ClosePlayer",true);
            dialogue.SetActive(true);
            PlayerController.myInstance.couldPickApple = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        anim.SetBool("ClosePlayer",false);
        dialogue.SetActive(false);
        PlayerController.myInstance.couldPickApple = false;
    }
}
