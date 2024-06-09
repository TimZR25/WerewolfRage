using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Firefly : MonoBehaviour
{
    [SerializeField] private float _radius;

    private NavMeshAgent _agent;

    private Vector3 _target;

    private Vector3 _startPosition;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _startPosition = transform.position;

        _target = _startPosition + _radius * GetRandomDirection();
        _agent.SetDestination(_target);
    }

    private void Update()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _target = _startPosition + _radius * GetRandomDirection();
            _agent.SetDestination(_target);
        }
    }

    private Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * _radius);
    }
}
