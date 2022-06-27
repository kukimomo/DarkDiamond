using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] items;
    public GameObject slotPrefab;

    [SerializeField]
    private CanvasGroup canvasGroup;
    private List<SlotScript> slots = new List<SlotScript>();

    public List<SlotScript> MySlotS
    {
        get
        {
            if (slots != null)
            {
                return slots;
            }

            return null;
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
            SlotScript slot=Instantiate(slotPrefab, gameObject.transform).GetComponent<SlotScript>();
            slots.Add(slot);
        }
    }

    private static Inventory instance;

    public static Inventory myInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Inventory>();
            }

            return instance;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Key k = (Key)Instantiate(items[0]);
            addItem(k);
            HealthPotion hp = (HealthPotion)Instantiate(items[1]);
            addItem(hp);
            Orb orb = (Orb)Instantiate(items[2]);
            addItem(orb);
            Staff staff = (Staff) Instantiate(items[3]);
            addItem(staff);
        }
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
            foreach (SlotScript slot in slots)
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
        foreach (SlotScript  slot in slots )
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
}
