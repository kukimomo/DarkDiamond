using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    private static HandScript instance;

    public static HandScript myInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }

            return instance;
        }
    }
    [SerializeField]
    private Vector3 offset;


    public Image icon;

    private SlotScript target;
    public SlotScript myTarget
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
            if (target == null)
            {
                icon.color = new Color(0, 0, 0, 0);
                icon.sprite = null;
            }
            else
            {
                icon.color = Color.white;
                icon.sprite = target.myItem.myIcon;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (icon.sprite != null)
        {
            icon.transform.position = Input.mousePosition+offset;
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()&&target!=null)
            {
                deleteItem();
            }
        }
        
    }

    public void setItemGrey()
    {
        myTarget.icon.color=Color.grey;
    }

    public void deleteItem()
    {
        target.myItemStack.Clear();
        target.refreshCount();
        myTarget = null;
    }
}
