using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyStateAttack : IState
{
    private NavMeshAgent _agent;

    private float _attackTime;
    private float _time;

    private Animator _animator;

    private int _damage;
    private float _attackRadius;
    private Transform _attackPoint;

    public UnityAction Attacked;

    public EnemyStateAttack(Enemy enemy)
    {
        _agent = enemy.Agent;
        _attackTime = enemy.AttackTime;
        _animator = enemy.Animator;
        _attackRadius = enemy.AttackRadius;
        _attackPoint = enemy.AttackPoint;
        _damage = enemy.Damage;
    }

    public void Enter()
    {
        _time = _attackTime;
        _animator.Play(Enemy.AnimationNames.Attack);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.transform.position, _attackRadius);

        foreach (Collider2D collider in colliders)
        {
            Debug.Log(collider);
            if (collider.TryGetComponent(out Player player))
            {
                player.ApplyDamage(_damage);
            }
        }
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
