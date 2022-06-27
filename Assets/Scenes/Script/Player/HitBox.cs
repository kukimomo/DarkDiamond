using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
        
            switch (other.gameObject.name)
            {
                case "BigEye":
                    other.GetComponent<BigEye>().getDamage(PlayerController.myInstance.damage);
                    break;

                case "Muddy":
                    other.GetComponent<Muddy>().getDamage(PlayerController.myInstance.damage);
                    break;
                case"Bat":
                    other.GetComponent<BatController>().getDamage(PlayerController.myInstance.damage);
                    break;
            }
        }
    }
}
