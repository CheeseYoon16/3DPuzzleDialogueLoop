using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class NPCWalkController : MonoBehaviour
{
    public LayerMask movementMask;

    PlayerController charControl;

    bool disabledMovement = false;

    [SerializeField] GameObject TargetDestination;
    bool gameCanStart = false;
    private Vector3 startPos = Vector3.zero;

    private void Awake()
    {
        gameCanStart = false;

    }

    private void Start()
    {
        startPos = transform.position;

        charControl = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!disabledMovement && gameCanStart)
        {
            if (TargetDestination != null && charControl != null)
            {
                charControl.MoveToPoint(TargetDestination.transform.position);
            }

        }
    }

    public void SetMovementDisabled(bool active)
    {
        disabledMovement = active;
    }

    public void ResetGame()
    {
        transform.position = startPos;
        gameCanStart = true;

    }

    public void StopGame()
    {
        gameCanStart = false;
    }


}
