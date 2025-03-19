using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMover : MonoBehaviour
{
    public NavMeshAgent _navAgent;
    float targetOffset = 0.1f;
    float moveDistance = 5.0f;

    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_navAgent.remainingDistance <= targetOffset)
        {
            Vector2 randomPoint = Random.insideUnitCircle * moveDistance; // Generates (X, Y) in a unit circle
            _navAgent.SetDestination(randomPoint);
        }
    }

    public void setTarget(Vector3 target)
    {
        _navAgent.SetDestination(target);
    }
}


