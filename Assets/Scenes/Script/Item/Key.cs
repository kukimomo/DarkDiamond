using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keyyy",menuName = "Items/Key",order=1)]
public class Key : Item
{
   public void submitKey()
   {
      if (couldUseItem)
      {
         Debug.Log("鑰匙被使用了" );
      }
   }
}
