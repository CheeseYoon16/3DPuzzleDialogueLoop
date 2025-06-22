using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TuruSore.BuildMode
{
    public class InputManager : MonoBehaviour
    {
        public Action<Vector3Int> onMouseClick, onMouseHold;
        public Action onMouseUp;

        [SerializeField]
        Camera mainCam;

        public LayerMask groundMask;

        private void Update()
        {
            CheckClickDownEvent();
            CheckClickUpEvent();
            CheckClickHoldEvent();
        }

        private void CheckClickHoldEvent()
        {
            if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                var position = RaycastGround();
                if (position != null)
                {
                    onMouseHold?.Invoke(position.Value);
                }
            }
        }

        private void CheckClickUpEvent()
        {
            if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                onMouseUp?.Invoke();
            }
        }

        private void CheckClickDownEvent()
        {
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                var position = RaycastGround();
                if (position != null)
                {
                    onMouseClick?.Invoke(position.Value);
                }
            }
        }

        private Vector3Int? RaycastGround()
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {
                Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
                return positionInt;
            }

            return null;
        }
    }
}

