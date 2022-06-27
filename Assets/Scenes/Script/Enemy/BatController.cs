using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : Enemy
{
    public Transform[] position;
    public Transform firePos;
    private Vector2 Destiantion;
    
    
    public float waitTime;
    public float startWaitTime;




    [Header("子弹相关")] 
    public int bulletId;
    public int bulletCount;
    public int bulletAngle;
    public GameObject bulletPrefab;

    
    private void Awake()
    {
        waitTime = startWaitTime;
        Destiantion = new Vector2(transform.position.x - 2f,transform.position.y-1f);
        
        InvokeRepeating("BulletAttack",3,5);
    }

    private void Update()
    {
        
        transform.position=Vector2.MoveTowards(transform.position,Destiantion,moveSpeed*Time.deltaTime);
        if(Vector2.Distance(transform.position,Destiantion)<0.1f)
        {
            if(waitTime<=0)
            {
                Destiantion=GetRandomPos();
                waitTime=startWaitTime;
            }
            else
            {
                waitTime-=Time.deltaTime;
            }
        }
    }
   

    public Vector2 GetRandomPos()
    {
        
        Vector2 movePos=new Vector2(Random.Range(position[0].position.x,position[1].position.x),
            Random.Range(position[0].position.y,position[1].position.y));
        return movePos;
    }

  
    public void BulletAttack()
    {
        for(int i =-bulletCount/2;i<bulletCount/2+1;i++)
        {
            GameObject tempBullet = Instantiate(bulletPrefab,transform.position,transform.rotation );
            tempBullet.GetComponent<SpriteRenderer>().sprite = tempBullet.GetComponent<Bullet>().sprite[bulletId];
            tempBullet.transform.Rotate(0,0,bulletAngle*i);
            tempBullet.AddComponent<Bullet>();
         
        }
    }

  
}
 