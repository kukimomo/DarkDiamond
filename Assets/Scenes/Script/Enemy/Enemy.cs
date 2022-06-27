using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
   
    public float moveSpeed;

    public float health;

    public float damage;

    public GameObject reward;

    public GameObject  bloodEffect;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody2D rigidbody;

    public Animator myAnimator
    {
        get
        {
            return animator;
        }
       
    }

    public Rigidbody2D myRigidbody2D
    {
        get
        {
            return rigidbody;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getDamage(float val)
    {
        health -= val;
        AudioManager.myInstance.PlaySound("敌人受伤");
        if (bloodEffect)
        {
            Instantiate(bloodEffect,transform);
        }
        
        if (health <= 0)
        {
            Instantiate(reward, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }

   
}
