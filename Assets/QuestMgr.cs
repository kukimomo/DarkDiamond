using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class QuestMgr : MonoBehaviour
{
   
   int[,] QuestGap = {{2, 11, 3}, { 15,10,7},{9,7,12},{8,1,14}}; 
   int[,] QuestAdd = {{2, 8, 4}, {2, 5, 0}, {10, 9, 8}};
   public CanvasGroup canvasGroup;
   public require[] requires;
   public GameObject questArea;
   public GameObject quest;
   private List<QuestScript> questList=new List<QuestScript>();
   private void Start()
   {
      canvasGroup = GetComponent<CanvasGroup>();
      for (int i = 0; i < QuestGap.GetLength(0); i++)
      {
         QuestScript q=Instantiate(quest, questArea.transform).GetComponent<QuestScript>();
         q.icon.sprite = requires[i].icon;
         q.index = i+1;
         q.curCount = 0;
         q.count = requires[i].count;
         q.name = requires[i].target;
         q.description.text = "任务"+q.index+":"+requires[i].content.ToString();
         q.info.text = requires[i].curCount.ToString() + "/" + requires[i].count.ToString();
         questList.Add(q);
         
         //如果拥有改物品，统计该物品此时的数量
         foreach (SlotScript  s in Inventory.myInstance.MySlotS)
         {
            if (s.myItemStack.Count > 0)
            {
               if (s.myItem.name ==requires[i].target)
               {
                  requires[i].curCount++;
               }
            }
         }
         q.info.text = requires[i].curCount.ToString() + "/" + requires[i].count.ToString();
        
      }
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.B))
      {
         for (int i = 0; i < questList.Count; i++)
         {
            questList[i].curCount = 0;
            //如果拥有改物品，统计该物品此时的数量
            foreach (SlotScript  s in Inventory.myInstance.MySlotS)
            {
               if (s.isEmpty) continue;
               if (s.myItem.name ==questList[i].name) //如果当前槽位的物品事目标物品
               {
                  questList[i].curCount+=s.myItemStack.Count;
               }
            }
            questList[i].info.text = questList[i].curCount.ToString() + "/" + questList[i].count.ToString();
         }
      }
   }

   public void openclose()
   {
      if (canvasGroup.alpha == 0)
      {
         canvasGroup.alpha = 1;
         canvasGroup.blocksRaycasts= true;
      }
      else
      {
         canvasGroup.alpha = 0;
         canvasGroup.blocksRaycasts = false;
      }
   }
}

[Serializable]
public class require
{
   public string content;//内容
   public int count;//目标数量
   public int curCount;//当前的数量
   public string target;//目标物品的名字
   public Sprite icon;//物品的图标
}
