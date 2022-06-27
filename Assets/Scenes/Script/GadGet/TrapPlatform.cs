using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapPlatform : MonoBehaviour
{
    private int id;

    public int myId
    {
        set
        {
            id = value;
        }
      
    }
    private Animator anim;
    private BoxCollider2D   bx2d; 
    void Start()
    {
        anim=GetComponent<Animator>();
        bx2d=GetComponent<BoxCollider2D>();
    }

    void Update()
    {
    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.gameObject.CompareTag("Player")) 
        {
           
            anim.SetTrigger("Collapse");
        }  
    }
    void DisableBoxCollider2D()
    {
        bx2d.enabled=false;
    }
    void DestroyTrapPlatform()
    {
        Debug.Log(id);
        SendMessageUpwards("create",id);
        Destroy(gameObject);
        
    }
}


