using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public float distance;
    private Vector2 curPos;

    private Vector3 Dir;

    public Vector3 myDir
    {
        get
        {
            return Dir;
        }
        set
        {
            Dir = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        curPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Dir*Time.deltaTime*speed);
        if (Vector2.Distance(curPos, transform.position) > distance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.myInstance.curHealth -= 1f;
            MessageBox.myInstance.showMessage(PlayerController.myInstance.gameObject.transform.position, "弓箭"+1.ToString(),messageType.Damage,1f);
            Destroy(gameObject);
        }
    }
}
