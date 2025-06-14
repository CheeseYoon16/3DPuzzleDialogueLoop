using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;

    bool disabledMovement = false;
    NavMeshAgent agent;

    [SerializeField] Animator characterAnimator;

    private void OnEnable()
    {
       agent = GetComponent<NavMeshAgent>();
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
                    MoveToPoint(hit.point);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    MoveToPoint(hit.point);
                }
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                OpenInventory();
            }

            if(agent!=null)
            {
                Vector3 worldVelocity = agent.velocity;

                // Convert world velocity to local direction (relative to character's forward)
                Vector3 localVelocity = transform.InverseTransformDirection(worldVelocity);

                // Set parameters for blend tree
                float speed = worldVelocity.magnitude;
                float directionX = localVelocity.x;
                float directionZ = localVelocity.z;

                characterAnimator.SetFloat(AnimationParamHolder.CharacterParam.CURRENT_SPEED_PARAM, speed);
                characterAnimator.SetFloat(AnimationParamHolder.CharacterParam.DIRECTION_X_PARAM, directionX);
                characterAnimator.SetFloat(AnimationParamHolder.CharacterParam.DIRECTION_Z_PARAM, directionZ);
            }
        }
    }

    public void SetMovementDisabled(bool active)
    {
        disabledMovement = active;
        if(disabledMovement)
        {
            SetAnimFloatParam(AnimationParamHolder.CharacterParam.CURRENT_SPEED_PARAM, 0);
        }
    }

    private void OpenInventory()
    {
        if(InventorySystem.Instance!=null)
        {
            InventorySystem.Instance.OpenInventory();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        if (disabledMovement) return;
        if (agent == null) return;

        agent.SetDestination(point);
    }

    public void SetAnimFloatParam(string param, float value)
    { 
        if(characterAnimator != null)
        {
            characterAnimator.SetFloat(param, value);
        }
    }

}
