using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public enum  messageType{Damage,Health,Mana,Enemy,Item}
public class MessageBox : MonoBehaviour
{
    [SerializeField]
    private GameObject messagePrefab;
    private static MessageBox instance;

    public static MessageBox myInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MessageBox>();
            }

            return instance;
        }
    }

    public void showMessage(Vector2 position, string text, messageType mtype,float offset)
    {
        position.y += offset;
        position.x += 1.5f;
        Text mgText = Instantiate(messagePrefab, transform).GetComponent<Text>();
        mgText.transform.position = position;
        string str1 = "";
        string str2 = "";
        switch (mtype)
        {
            case messageType.Damage:
                str1 = "受到了";
                str2 = "点伤害";
                mgText.color=Color.red;
                break;
            case messageType.Item:
                str1 = "获得了";
                str2 = "+1";
                mgText.color = Color.white;
                break;
            case messageType.Health :
                str1 = "生命值+";
                mgText.color=Color.green;
                break;
            case messageType.Mana:
                str1 = "魔法值+";
                mgText.color=Color.blue;
                break;
        }
        mgText.text =str1+ text+str2 ;
    }
    
}
