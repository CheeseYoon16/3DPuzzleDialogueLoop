using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;

    PlayerMotor motor;

    bool disabledMovement = false;

    void Start()
    {
       motor = GetComponent<PlayerMotor>();
    }
    void Update()
    {
        if(!disabledMovement)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, movementMask))
                {
                    motor.MoveToPoint(hit.point);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    motor.MoveToPoint(hit.point);
                }
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                OpenInventory();
            }
        }
    }

    public void SetMovementDisabled(bool active)
    {
        disabledMovement = active;
    }

    private void OpenInventory()
    {
        if(InventorySystem.Instance!=null)
        {
            InventorySystem.Instance.OpenInventory();
        }
    }

        
}
