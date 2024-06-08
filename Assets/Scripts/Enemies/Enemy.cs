using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _player;

    private NavMeshAgent _agent;

    private StateMachine _stateMachine;

    private EnemyStateRun _stateRun;
    private EnemyStateAttack _stateAttack;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _stateMachine = new StateMachine();
        _stateRun = new EnemyStateRun(_player, _agent);
        _stateAttack = new EnemyStateAttack(_agent);

        _stateAttack.Attacked += OnAttacked;

        _stateMachine.Initialize(_stateRun);
    }

    private void Update()
    {
        _stateMachine.CurrentState.Update();

        if (Vector2.Distance(transform.position, _player.transform.position) <= _agent.stoppingDistance)
        {
            if (_stateMachine.CurrentState == _stateAttack)
                return;

            _stateMachine.ChangeState(_stateAttack);
        }
    }

    private void OnAttacked()
    {
        _stateMachine.ChangeState(_stateRun);
    }
}
