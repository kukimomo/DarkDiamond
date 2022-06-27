using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  stateType
{
    Idle,Chase,GetHit,Patrol,React,Attack
}

[Serializable]
public class Parameter
{
   public float runSpeed;
   public float chaseSpeed;
   public float attackRange;
   public float idleTime;
   public Transform[] chasePos;
   public Transform[] patrolPos;
   public Transform attackPoint;
   public Transform target;
   public Animator animator;
   public LayerMask layer;
}
public class FSM : MonoBehaviour
{
    public Parameter parameter;
    private IState currentState;
    private Dictionary<stateType, IState> states = new Dictionary<stateType, IState>();

    private void Awake()
    {
        states.Add(stateType.Idle,new IdleState(this));
        states.Add(stateType.Patrol,new PatrolState(this));
        states.Add(stateType.Attack,new AttackState(this));
        states.Add(stateType.Chase,new ChaseState(this));
        states.Add(stateType.React,new ReactState(this));
        states.Add(stateType.GetHit,new GetHitState(this));
       
        TransitionState(stateType.Idle);
    }

    private void Update()
    {
      
        currentState.OnUpdate();
    }
    public void TransitionState(stateType state)
    {
        if (currentState != null)
        
            currentState.OnExit();
        

        currentState = states[state];
        currentState.OnEnter();

    }

    public void flipTo(Transform target)
    {
        if (target != null)
        {
            if (target.position.x - transform.position.x > 0)
            {
                
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
              
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = null;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(parameter.attackPoint.position,parameter.attackRange);
    }
}