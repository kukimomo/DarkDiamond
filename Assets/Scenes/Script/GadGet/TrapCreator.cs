using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapCreator : MonoBehaviour
{
    public GameObject trapPlatformPrefab;

    public float time;
    public List<Transform> children=new List<Transform>();
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        children = gameObject.GetComponentsInChildren<Transform>().ToList();
        children.Remove(children[0]);
        for (int i=0; i < children.Count;i++)
        {
            GameObject g=Instantiate(trapPlatformPrefab, children[i],false);
            g.GetComponent<TrapPlatform>().myId = i;

        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    IEnumerator trapRestart(int id)
    {
        //yield return new WaitForSeconds(time);

        yield return new WaitForSeconds(time);
        GameObject g=Instantiate(trapPlatformPrefab, children[id],false);
        g.GetComponent<TrapPlatform>().myId = id;
    }

   

    void create(int id)
    {
        StartCoroutine(trapRestart(id));
    }
}
