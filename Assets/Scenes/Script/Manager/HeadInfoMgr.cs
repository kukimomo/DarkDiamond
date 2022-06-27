using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeadInfoMgr : MonoBehaviour
{
    public Image healthImage;
    public Image manaImage;
    public Text healthText;
    public Text manaText;

   
    public Text coinText;
    void Start()
    {
        healthText.text = PlayerController.myInstance.curHealth.ToString()+"/"+PlayerController.myInstance.maxHealth.ToString();
        manaText.text = PlayerController.myInstance.curMana.ToString() + "/" +
                        PlayerController.myInstance.maxMana.ToString();
        healthImage.fillAmount=PlayerController.myInstance.curHealth/PlayerController.myInstance.maxHealth;
        manaImage.fillAmount = PlayerController.myInstance.curMana/PlayerController.myInstance.maxMana;
        coinText.text = "金币：0";
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.myInstance.curHealth >= PlayerController.myInstance.maxHealth)
        {
            PlayerController.myInstance.curHealth = PlayerController.myInstance.maxHealth;
        }

        if (PlayerController.myInstance.curMana >= PlayerController.myInstance.maxMana)
        {
            PlayerController.myInstance.curMana = PlayerController.myInstance.maxMana;
        }

    healthText.text = PlayerController.myInstance.curHealth.ToString()+"/"+PlayerController.myInstance.maxHealth.ToString();
        manaText.text = PlayerController.myInstance.curMana.ToString() + "/" +
                        PlayerController.myInstance.maxMana.ToString();
        healthImage.fillAmount=PlayerController.myInstance.curHealth/PlayerController.myInstance.maxHealth;
        manaImage.fillAmount = PlayerController.myInstance.curMana/PlayerController.myInstance.maxMana;
        coinText.text = "金币：" + PlayerController.myInstance.myMoney;
    }
}
