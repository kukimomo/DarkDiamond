using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public float disappearTime;
    void Start()
    {
        Destroy(gameObject,disappearTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
