using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;

public class SlotScript : MonoBehaviour,IPointerClickHandler
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
        item.mySlot = this;
        MessageBox.myInstance.showMessage(PlayerController.myInstance.transform.position,item.name,messageType.Item,1.0f);
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
        if (eventData.button==PointerEventData.InputButton.Left)
        {
            if (!isEmpty&&HandScript.myInstance.myTarget==null)//初次点击
            {
                HandScript.myInstance.myTarget = this;
                HandScript.myInstance.setItemGrey();
            }
            else if (!isEmpty && HandScript.myInstance.myTarget != null)
            {
                if (HandScript.myInstance.myTarget == this)//1.放回原位  
                {
                    icon.color=Color.white;
                    HandScript.myInstance.myTarget = null;
                }
                else if (HandScript.myInstance.myTarget!=this)//2.融合
                {
                    if (HandScript.myInstance.myTarget.myItem.name == myItem.name) //2.1如果这两个是同一种东西
                    {
                        int before = HandScript.myInstance.myTarget.myItemStack.Count;
                        int cur = myItemStack.Count;
                        if (before + cur <= myItem.myCount) //2.1.1两种东西数量小于  该物品的  count  
                        {
                            for (int i = 0; i < before; i++)
                            {
                                addItem(myItem);
                            }
                            HandScript.myInstance.myTarget.myItemStack.Clear();
                            HandScript.myInstance.myTarget.refreshCount();
                            HandScript.myInstance.myTarget = null;
                        }
                        else//2.1.2两种东西数量大于 该物品的 count
                        {
                            int val;
                            if (cur == myItem.myCount && before == myItem.myCount)
                            {
                                HandScript.myInstance.myTarget.icon.color = Color.white;
                            }
                            else if (cur == myItem.myCount)
                            {
                                val = myItem.myCount - before;
                                for (int i = 0; i < val; i++)
                                {
                                    popItem();
                                    HandScript.myInstance.myTarget.addItem(myItem);
                                }
                            }
                            else
                            {
                                val = myItem.myCount-cur;
                                for (int i = 0; i < val; i++)
                                {
                                    addItem(myItem);
                                    HandScript.myInstance.myTarget.popItem();
                                }
                            }
                            HandScript.myInstance.myTarget = null;
                        }
                    }
                    else //2.2  如果这两个东西不是同一种东西  
                    {
                        Item item1 = HandScript.myInstance.myTarget.myItem;
                        Item item2 = myItem;
                        int val1 = HandScript.myInstance.myTarget.myItemStack.Count;
                        int val2 = myItemStack.Count;
                        HandScript.myInstance.myTarget.myItemStack.Clear();
                        myItemStack.Clear();
                        for (int i = 0; i < val1; i++)
                        {
                            addItem(item1);
                        }

                        for (int i = 0; i < val2; i++)
                        {
                            HandScript.myInstance.myTarget.addItem(item2);
                        }
                        HandScript.myInstance.myTarget = null;
                    }
                }
                
            }
            else if (isEmpty && HandScript.myInstance.myTarget != null)//3.将物品放到空位置
            {
                //记录上一个位置的物品和数量
                Item item = HandScript.myInstance.myTarget.myItem;
                int count = HandScript.myInstance.myTarget.myItemStack.Count;
                //清空上一个位置
                HandScript.myInstance.myTarget.myItemStack.Clear();
                HandScript.myInstance.myTarget.refreshCount();
                
                //手的图标清空
                HandScript.myInstance.myTarget = null;
                
                //新的位置数量刷新
               for (int i = 0; i < count; i++)
                {
                   addItem(item); //additem里包括refresh的方法了
                }
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
          useItem();  
        }
    }
}
