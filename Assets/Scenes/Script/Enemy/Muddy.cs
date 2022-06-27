using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muddy : Enemy
{
    public float distance;

    public float waitTime;
    public Transform arrowPos;
    public GameObject arrowPrefab;
    private float recordTime;
    private bool canGenerate = false;
    private Vector2 leftPos;
    private Vector2 rightPos;
    private Vector2 targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        leftPos = new Vector2(transform.position.x - distance, transform.position.y);
        rightPos = new Vector2(transform.position.x + distance, transform.position.y);
        targetPos = leftPos;
        recordTime = waitTime;
        transform.localScale = targetPos.x - transform.position.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        myAnimator.SetBool("run",true);
        if (Vector2.Distance(transform.position, targetPos) < 0.05)
        {
            waitTime -= Time.deltaTime;
            myAnimator.SetBool("run",false);
           
            if (waitTime <= 0)
            {
                GameObject g=Instantiate(arrowPrefab, arrowPos.position, Quaternion.identity);
                g.transform.localScale = transform.localScale;
                if (g.transform.localScale == new Vector3(1, 1, 1))
                {
                    g.GetComponent<Arrow>().myDir=Vector3.right;
                }
                else
                {
                    g.GetComponent<Arrow>().myDir=Vector3.left;
                }
                
                waitTime = recordTime;
                targetPos = targetPos.x- leftPos.x==0 ? rightPos : leftPos;
                transform.localScale = transform.localScale == new Vector3(1, 1, 1)
                    ? new Vector3(-1, 1, 1)
                    : new Vector3(1, 1, 1);
            }
        }
        
    }

  
}
