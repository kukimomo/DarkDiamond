using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : ScriptableObject
{
    [SerializeField]
    private int count;

   


    public string name;


    public int price;
    
    [SerializeField]
    private Sprite icon;
    
    private SlotScript slot;
    private shopSlotScript shopSlot;
 
    public Sprite myIcon
    {
        get
        {
            return icon;
        }
    }
    public int myCount
    {
        get
        {
            return count;
        }
    }

    public SlotScript mySlot
    {
        get
        {
            return slot;
        }
        set
        {
            slot = value;
        }
    }

    public shopSlotScript myShopSlot
    {
        get
        {
            return shopSlot;
        }
        set
        {
            shopSlot = value;
        }
    }

    public bool couldUseItem
    {
        get
        {
            if (!mySlot.isEmpty)
            {
                mySlot.myItemStack.Pop();
                mySlot.refreshCount();
                return true;
            }
            return false;
        }
    }
}
