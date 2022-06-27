using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "orb",menuName ="Items/Orb",order=3)]
public class Orb : Item,IUseable
{
    // Start is called before the first frame update
    public void use()
    {
        if (couldUseItem)
        {
            PlayerController.myInstance.curHealth += 3;
            PlayerController.myInstance.curMana += 10;
            MessageBox.myInstance.showMessage(PlayerController.myInstance.gameObject.transform.position, 3.ToString(),messageType.Health,1f);
            MessageBox.myInstance.showMessage(PlayerController.myInstance.gameObject.transform.position, 10.ToString(),messageType.Mana,2f);
        }
    }
}
