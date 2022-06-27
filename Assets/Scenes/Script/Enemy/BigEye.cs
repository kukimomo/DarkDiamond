using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEye : Enemy
{
    public float moveDis;
    private Vector2 startPos;
    private Vector2 endPos;

    private Vector2 targetPos;
    
    public float waitTime;
    private float recordTime;
    
    void Start()
    {
        startPos = new Vector2(transform.position.x -moveDis, transform.position.y);
        endPos = new Vector2(transform.position.x + moveDis, transform.position.y);
        targetPos = startPos;
        recordTime = waitTime;
    }

 
    void Update()
    {
        transform.localScale = targetPos.x >transform.position.x?new Vector3(1,1,1):new Vector3(-1,1,1);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        myAnimator.SetBool("run",true);
        if (Math.Abs(transform.position.x - targetPos.x) < 0.05f)
        {
            myAnimator.SetBool("run",false);
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                waitTime = recordTime;
                targetPos = targetPos == startPos ? endPos : startPos;
            }
        }
    }
}
