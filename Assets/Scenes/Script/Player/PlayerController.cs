using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody2D myRigidbody;
    [SerializeField]
    [Header("人物基础信息")]
    private float money;//金币
    public float maxHealth;//最大生命
    public float curHealth;//当前生命
    public float maxMana;//最大魔法
    public float curMana;//当前魔法
    public float damage;//攻击
   

    private float input;//水平轴方向的速度
    private bool isAttack = false;
    private bool isOnGound;
    private int comboStep;//普通攻击连招
    public float moveSpeed;//移动速度
    public float radius;//脚下踩的半径  用来检测脚底下的层级
    public float comboSpeed;//普通连招时时给的位移
    public float timer;//多久可以进行一次普通攻击
    private float interval = 1f;
    public float jumpSpeed;//跳跃的高度
    public CastManager playerCast;
    public GameObject wavePrefab;
    public Transform wavePos;
    public GameObject fruitPrefab;
    public Transform fruitPos;
    public Sprite[] fruits;
    public bool couldPickApple;
    [Header("杂项信息")]
    private Vector3 flipTo;
    private Coroutine castRountine;
    
    
    [Header("冲刺相关")] 
    public float dashSpeed;
    public float dashTime;
    float startDashTimer;
    bool isDashing;
    public GameObject dashObj;


    private static PlayerController instance;

    public static PlayerController myInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
            }

            return instance;
        }
    }


    public float myMoney
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
        }
    }

    public string castAnimatorContionName         //蓄力的动画条件名
    {
        get;
        set;
    }

    public bool isCasting
    {
        get;
        set;
    }
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    
  
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        isOnGound = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y-0.88f), radius, LayerMask.GetMask("Ground","Platform"));
        myAnimator.SetFloat("Horizontal",Math.Abs(myRigidbody.velocity.x));
        myAnimator.SetFloat("Vertical", myRigidbody.velocity.y);
        myAnimator.SetBool("IsOnGounrd",isOnGound);
        Flip();
        if (isCasting == true && (Mathf.Abs( myRigidbody.velocity.x) > 0.05f||Mathf.Abs( myRigidbody.velocity.y) > 0.05f))
        {
            stopAction();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            castAnimatorContionName = "castWave";
            castRountine=StartCoroutine(castCoroutine(0, castAnimatorContionName, 2f, wavePrefab, wavePos));
        }

        if (Input.GetKeyDown(KeyCode.P)&&couldPickApple)
        {
            
            castAnimatorContionName = "dropFruit";
            castRountine = StartCoroutine(castCoroutine(1, castAnimatorContionName, 1f, fruitPrefab, fruitPos));

        }
        

        
      
    }

    
  
    private void FixedUpdate()
    {
        Move();
        Jump();
        Attack();
        dash();
    }

    #region 移动 跳跃 翻转 攻击
    void Move()
    {
        if (!isAttack)
        {
            myRigidbody.velocity = new Vector2(input * moveSpeed, myRigidbody.velocity.y);
        }
        else
        {
            myRigidbody.velocity = new Vector2(transform.localScale.x*comboSpeed,myRigidbody.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.K) && isOnGound)
        {
            myRigidbody.velocity = new Vector2(0, jumpSpeed);
            myAnimator.SetTrigger("Jump");
        }
    }
  
    void Flip()
    {
        if (input != 0)
        {
            if (input > 0.01f)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                flipTo=Vector3.right;
            }
            else if (input < -0.01f)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                flipTo=Vector3.left;
            }
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttack)
        {
            isAttack = true;
            comboStep++;
            if (comboStep > 4)
            {
                comboStep = 1;
            }

            timer = interval;
            myAnimator.SetInteger("ComboStep",comboStep);
            myAnimator.SetTrigger("Attack");
        }

        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                comboStep = 0;
            
            }
        }
      
    }

    public void ResetAttack()
    {
        isAttack = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x,transform.position.y-0.88f),radius);
    }
    
    //冲刺
    private  void dash()
    {
        if (!isDashing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               
                dashObj.SetActive(true);
                isDashing = true;
                startDashTimer = dashTime;
            }
        }
        else
        {
            startDashTimer -= Time.deltaTime;
            if (startDashTimer <= 0)
            {
                
                isDashing = false;
                dashObj.SetActive(false);
            }
            else
            {
                myRigidbody.velocity = transform.right * dashSpeed*transform.localScale.x ;
            }
        }
        
    }
    #endregion

    //蓄力技能
    //castItem的index，触发的动画bool名字，蓄力多久，生成的预制体，生成预制体的位置
    IEnumerator castCoroutine(int index,string animatorConditoinName,float castTime,GameObject prefab,Transform pos)
    {
        isCasting = true;
        CastItem item = playerCast.castSomething(index);
        myAnimator.SetBool(animatorConditoinName,true);
        yield return new WaitForSeconds(castTime);
        myAnimator.SetBool(animatorConditoinName,false);
        
        GameObject g=Instantiate(prefab,pos.position , Quaternion.identity);
        curMana -= 15;
        if (g.name == "wave(Clone)")
        {
            g.GetComponent<waveAttack>().setDirAndLocalScale(GetFlipTo(),20,13);
        }
        else if (g.name.Contains("Fruit"))
        {
            g.GetComponent<SpriteRenderer>().sprite = fruits[Random.Range(0, fruits.Length)];
        }
        stopAction();
    }

    public void stopAction()//暂停蓄力
    {
        playerCast.stopRoutinue();
        myAnimator.SetBool(castAnimatorContionName,false);
        if (castRountine != null)
        {
            StopCoroutine(castRountine);
            castRountine = null;
        }

        isCasting = false;
    }
    //玩家此刻的朝向
    public Vector3 GetFlipTo()
    {
        return transform.localScale == new Vector3(1, 1, 1) ? Vector3.right : Vector3.left;
    }

  

    
    private void OnTriggerExit2D(Collider2D other)
    {
        TextManager.myInstance.myInputAble = false;
        TextManager.myInstance.dialogueLong.gameObject.SetActive(false);
    }

   
}
