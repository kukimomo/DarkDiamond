using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    
    public Text description;//任务的描述
    public Image icon;//目标对象的图标
    public Text info;//收集的数量情况
    public int[] gap;
    public int index;//当前任务的下标
    public int curCount;
    public int count;//目标数量
    public string name;
    public void Awake()
    {
        
    }
}
