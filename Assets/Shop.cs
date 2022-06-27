using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Shop : MonoBehaviour
{
    private static Shop instance;

    public static Shop myInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Shop>();
            }

            return instance;
        }
    }

    public Item[] items;
    public GameObject slotPrefab;
    public GameObject buyPanel;
    [Header("购买界面")] 
    public GameObject panel;
    public Text content;
    public Text cost;
    public Button addBtn;
    public Button subBtn;
    public Button buyBtn;
    [SerializeField]
    private CanvasGroup canvasGroup;
    private List<shopSlotScript> slots = new List<shopSlotScript>();
    private int cur=1;

    public int myCur
    {
        get
        {
            return cur;
        }
        set
        {
            cur = value;
        }
    }
    private shopSlotScript shopSlot;

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
    // Start is called before the first frame update
    void Start()
    {
        //items[0] = Resources.Load<Item>("Keyyy");
        //items[1] = Resources.Load<Item>("healthPotions");
        canvasGroup = GetComponent<CanvasGroup>();
        for (int i = 0; i < 25; i++)
        {
            shopSlotScript slot=Instantiate(slotPrefab, gameObject.transform).GetComponent<shopSlotScript>();
            slots.Add(slot);
        }

        for (int i = 0; i < 3; i++)
        {
            HealthPotion hp = (HealthPotion) Instantiate(items[0]);
            addItem(hp);
        }

        for (int i = 0; i < 4; i++)
        {
            Key k = (Key) Instantiate(items[1]);
            addItem(k);
        }

        for (int i = 0; i < 4; i++)
        {
            Orb o = (Orb) Instantiate(items[2]);
            addItem(o);
        }

        for (int i = 0; i < 4; i++)
        {
            Staff s = (Staff) Instantiate(items[3]);
            addItem(s);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    
    
  
    
    public void addItem(Item item)
    {
        
        if (item.myCount < 2)//item的最大容量就是1，只需要找到第一个empty的slot存放
        {
            placeInEmpty(item);
        }
        else
        {
            /* item的最大容量>=2
               1.找到目标格子  加进去
               2.slot还没有这个东西，需要用一个empty的slot装*/
            foreach (shopSlotScript slot in slots)
            {
           
                if (!slot.isEmpty&&item.name == slot.myItemStack.Peek().name && !slot.isFull)
                {
                    slot.addItem(item);
                    return;
                }
                
            }
          
            placeInEmpty(item);
            
        }
        
    }

    public void placeInEmpty(Item item)
    {
        foreach (shopSlotScript  slot in slots )
        {
            if (slot.isEmpty)
            {
                slot.addItem(item);
                return;
            }
        }
        Debug.Log("格子不够了");
        return;
    }

    public void OpenClose()
    {
        if (canvasGroup.alpha == 1)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
        AudioManager.myInstance.PlaySound("open");
    }

    public void BuyItem(shopSlotScript ss,int curCount)
    {
        if (!buyPanel.activeSelf)
        {
            buyPanel.SetActive(true);
        }
        int count = ss.myItemStack.Count;
        int price = ss.myItem.price;
        string name = ss.myItem.name;
        content.text = "你是否想要购买" + curCount.ToString() + name+"?";
        cost.text = (price*curCount) .ToString();
    }

    
    public void add()
    {
        if (myShopSlot!=null&&cur < myShopSlot.myItemStack.Count)
        {
            cur++;
            BuyItem(myShopSlot,cur);
        }
    }

    public void sub()
    {
        if (myShopSlot!=null&&cur > 1)
        {
            cur--;
            BuyItem(myShopSlot,cur);
        }
    }

    public void cancel()
    {
        if (buyPanel.activeSelf==true)
        {
            buyPanel.SetActive(false);
        }
    }
    
    public void buy()
    {
        if (myShopSlot==null)
        {
            if (buyPanel.activeSelf==true)
            {
                buyPanel.SetActive(false);
            }
            return;
        }
        if (PlayerController.myInstance.myMoney >=cur*myShopSlot.myItem.price&&cur<=myShopSlot.myItemStack.Count)
        {
            PlayerController.myInstance.myMoney -= cur * myShopSlot.myItem.price;
            for (int i = 0; i < cur; i++)
            {
                Inventory.myInstance.addItem(myShopSlot.myItem);
                myShopSlot.popItem();
            }

            if (myShopSlot.myItemStack.Count == 0)
            {
                if (buyPanel.activeSelf==true)
                {
                    buyPanel.SetActive(false);
                }
                myShopSlot = null;
            }
        }
    }

   
}


