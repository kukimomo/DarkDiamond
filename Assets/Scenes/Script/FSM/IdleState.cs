using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private float timer;
    public FSM manager;
    public Parameter parameter;
    public IdleState(FSM fsm)
    {
        this.manager = fsm;
        this.parameter = fsm.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Idle");
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if (parameter.target != null && parameter.target.position.x > parameter.patrolPos[0].position.x &&
            parameter.target.position.x < parameter.patrolPos[1].position.x)
        {
            manager.TransitionState(stateType.React);
        }
        
        if (timer > parameter.idleTime)
        {
            manager.TransitionState(stateType.Patrol);
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}
public class PatrolState : IState
{
    public FSM manager;
    public Parameter parameter;

    public int index;
    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
       parameter.animator.Play("Walk"); 
    }

    public void OnUpdate()
    { 
     
        manager.flipTo(parameter.patrolPos[index]);
        manager.transform.position = Vector2.MoveTowards
            (manager.transform.position, parameter.patrolPos[index].position,
            parameter.runSpeed*Time.deltaTime);
     
        if (parameter.target != null && parameter.target.position.x > parameter.chasePos[0].position.x&&parameter.target.position.x<parameter.chasePos[1].position.x)
        {
      
            manager.TransitionState(stateType.React);
        }
        if (Vector2.Distance(manager.transform.position, parameter.patrolPos[index].position) < 0.1f)
        {
            manager.TransitionState(stateType.Idle);
        }
        
        
    }

    public void OnExit()
    {
        index++;
        if (index ==parameter.chasePos.Length)
        {
            index = 0;
        }
    }
}

public class ChaseState:IState
{
    public FSM manager;
    public Parameter parameter;
    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Walk");
    }

    public void OnUpdate()
    {
        manager.flipTo(parameter.target);
       
        if (parameter.target)
        {
            manager.transform.position =
                Vector2.MoveTowards(manager.transform.position, new Vector2(parameter.target.transform.position.x,manager.transform.position.y),
                    parameter.chaseSpeed * Time.deltaTime);
            Debug.Log("chasestate update toward targets");
        }

        if (parameter.target == null || manager.transform.position.x < parameter.chasePos[0].position.x ||
            manager.transform.position.x > parameter.chasePos[1].position.x)
        {
            manager.TransitionState(stateType.Idle);
        }

        if (Physics2D.OverlapCircle(parameter.attackPoint.position, parameter.attackRange, parameter.layer))
        {
            manager.TransitionState(stateType.Attack);
        }

    }

    public void OnExit()
    {
       
    }
}

public class ReactState:IState
{
    public FSM manager;
    public Parameter parameter;
    private AnimatorStateInfo info;
    public ReactState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("React");
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.95f)
        {
            manager.TransitionState(stateType.Chase);
        }
    }

    public void OnExit()
    {
       
    }
}

public class GetHitState:IState
{
    public FSM manager;
    public Parameter parameter;
    public GetHitState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {
       
    }

    public void OnExit()
    {
       
    }
}

public class AttackState:IState
{
    public FSM manager;
    public Parameter parameter;
    private AnimatorStateInfo info;
 
    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Attack");
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime > 0.95f)
        {
            manager.TransitionState(stateType.Chase);
        }
    }

    public void OnExit()
    {
       
    }
}

