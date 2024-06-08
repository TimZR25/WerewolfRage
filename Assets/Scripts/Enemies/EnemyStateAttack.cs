using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyStateAttack : IState
{
    private NavMeshAgent _agent;

    private float _attackTime;
    private float _time;

    public UnityAction Attacked;

    public EnemyStateAttack(NavMeshAgent agent, float attackTime)
    {
        _agent = agent;
        _attackTime = attackTime;
    }

    public void Enter()
    {
        _time = _attackTime;

        Debug.Log("Attack");
    }

    public void Exit()
    {
        _agent.isStopped = false;
    }

    public void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
            return;
        }

        Attacked?.Invoke();
    }
}
