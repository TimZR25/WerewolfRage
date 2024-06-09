using UnityEngine;
using UnityEngine.AI;

public class EnemyStateRun : IState
{
    private Player _player;
    private NavMeshAgent _agent;

    public EnemyStateRun(Player player, NavMeshAgent navMeshAgent)
    {
        _player = player;
        _agent = navMeshAgent;

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    public void Enter()
    {
        _agent.isStopped = false;
    }

    public void Exit()
    {
        _agent.isStopped = true;
    }

    public void Update()
    {
        if (_agent.isStopped == false)
            _agent.SetDestination(new Vector2(_player.transform.position.x, _player.transform.position.y));

    }
}

