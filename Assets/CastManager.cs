using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CastItem
{
   public Sprite CIcon;
   public string CName;
   public float CDuration;
   public Color fillColor;

}

public class CastManager : MonoBehaviour
{
    public CastItem[] items;
    public Image icon;//图标
    public Text name;//名称
    public Text restTime;//持续时间
    public Image fillBar;//渐变的进度条
    public CanvasGroup canvasGroup;
    private  Coroutine  curRountine;
    private Coroutine  fadeRoutine;
    public CastItem castSomething(int index)
    {
        icon.sprite = items[index].CIcon;
        name.text = items[index].CName;
        restTime.text = items[index].CDuration.ToString();
        fillBar.fillAmount = 0;
        fillBar.color = items[index].fillColor;
        
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        curRountine=StartCoroutine(progress(index));
        fadeRoutine=StartCoroutine(fadeBar());
        return items[index];
    }

    IEnumerator progress(int index)
    {
        float timer = Time.deltaTime;
        float rate = 1 / items[index].CDuration;
        float progress = 0;
        while (progress < 1)
        {
            fillBar.fillAmount = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            timer += Time.deltaTime;
            
            restTime.text = (items[index].CDuration - timer).ToString("F2");
            if (items[index].CDuration - timer < 0.03)
            {
                restTime.text = 0.ToString();
            }
            yield return null;
          
        }
       
        stopRoutinue();
    }

    IEnumerator fadeBar()
    {
        float rate = 1.0f / 0.5f;
        float progress = 0.0f;
        while (progress <= 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }
    }
    
    public void stopRoutinue()
    {
        if (curRountine != null)
        {
            StopCoroutine(curRountine);
            curRountine = null;
        }

        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            canvasGroup.alpha = 0;
            fadeRoutine = null;
        }
    }

   
}
