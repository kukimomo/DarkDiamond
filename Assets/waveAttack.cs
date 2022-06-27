using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class waveAttack : MonoBehaviour
{
    [SerializeField]
    private Vector3 dir;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float distacne;

    [SerializeField]
    private Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir*speed*Time.deltaTime);
        if (Vector2.Distance(pos ,transform.position) > distacne)
        {
            Destroy(gameObject);
        }
        
    }

    public void setDirAndLocalScale(Vector3 flipto,float Speed,float dis )
    {
        dir = flipto;
        transform.localScale = flipto == Vector3.left ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        this.speed = Speed;
        this.distacne = dis;
    }
}
