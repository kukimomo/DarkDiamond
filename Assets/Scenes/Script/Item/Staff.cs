using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "staff",menuName = "Items/Staff",order  =4)]
public class Staff : Item,IUseable
{
    public void use()
    {
        if (couldUseItem)
        {
            Debug.Log("使用法杖了");
        }
    }
}
