using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "healthPotions",menuName = "Items/HealthPotion",order=2)]
public class HealthPotion : Item,IUseable
{
   public void use()
   {
      if (couldUseItem)
      {
         PlayerController.myInstance.curHealth += 5;
         MessageBox.myInstance.showMessage(PlayerController.myInstance.gameObject.transform.position, 5.ToString(),messageType.Health,1f);
      }
   }
}
