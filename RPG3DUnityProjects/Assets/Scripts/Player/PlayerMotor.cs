using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;

    bool disabledMovement = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint (Vector3 point)
    {
        if(disabledMovement) return;
        agent.SetDestination(point);
    }

    public void SetMovementDisabled(bool active)
    {
        disabledMovement = active;
    }
}
