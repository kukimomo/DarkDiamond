using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shopSlotScript : MonoBehaviour,IPointerClickHandler
{
    public Image icon;

    public Text count;
    private Stack<Item> itemStack = new Stack<Item>();
    private IPointerClickHandler _pointerClickHandlerImplementation;

    public Stack<Item> myItemStack
    {
        get
        {
            return itemStack;
        }
    }
    
    public bool isEmpty
    {
        get
        {
            return itemStack.Count == 0;
        }
    }

    public Item myItem
    {
        get
        {
            if (!isEmpty)
            {
                return myItemStack.Peek();
            }

            return null;
        }
    }
    public bool isFull
    {
        get
        {
            return myItemStack.Count ==myItemStack.Peek().myCount;
        }
    }
    public void addItem(Item item)
    {
        icon.color = Color.white;
        icon.sprite = item.myIcon;
        item.myShopSlot = this;
       
        myItemStack.Push(item);
        refreshCount();
    }


    public void popItem()
    {
        if (!isEmpty)
        {
            myItemStack.Pop();
        }
        refreshCount();
    }
    public void useItem()
    {
        if (myItem is IUseable)
        {
            (myItem as IUseable).use();
        }
    }

    public void refreshCount()
    {
        if (myItemStack.Count == 0)
        {
            icon.color = new Color(0, 0, 0, 0);
            count.text = null;
        }
        else if (myItemStack.Count==1)
        {
            icon.color=Color.white;
            count.text = null;
        }
        else if (myItemStack.Count >= 2)//只有数量大于2 才会显示数量
        {
            icon.color=Color.white;
            count.text = myItemStack.Count.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Shop.myInstance.myShopSlot==null&&eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("itemStack count==="+itemStack.Count);
            if (itemStack.Count == 0) return;
            Shop.myInstance.myShopSlot = this;
            Shop.myInstance.myCur = 1;
            Shop.myInstance.BuyItem(this,1);
        }
        else if (Shop.myInstance.myShopSlot != null)
        {
            if (itemStack.Count == 0) return;
            Shop.myInstance.myShopSlot = this;
            Shop.myInstance.myCur = 1;
            Shop.myInstance.BuyItem(this,1);
        }
    }
    

}
