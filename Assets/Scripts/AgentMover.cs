using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{
    public NavMeshAgent _navAgents;
    public float offset = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _navAgents = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo))
        {
            var targetPosition = hitInfo.point;
            _navAgents.SetDestination(targetPosition);
        }
    }
}