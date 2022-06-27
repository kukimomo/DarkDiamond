using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float speed ;
    public Sprite[] sprite;

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*Time.deltaTime*speed);
        if (transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("子弹打到了地面");
            Destroy(gameObject);
        }
       
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.myInstance.curHealth -= 1f;
            MessageBox.myInstance.showMessage(PlayerController.myInstance.gameObject.transform.position, "子弹"+1.ToString(),messageType.Damage,1f);
            Destroy(gameObject);
        }
    }
}
