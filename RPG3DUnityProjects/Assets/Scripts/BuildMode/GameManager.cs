using System;
using System.Collections;
using System.Collections.Generic;
using SVS;
using UnityEngine;

namespace TuruSore.BuildMode
{
    public class GameManager : MonoBehaviour
    {
        public InputManager inputManager;
        public RoadManager roadManager;

        private void OnEnable()
        {
            inputManager.onMouseClick += HandleMouseClick;
        }

        private void HandleMouseClick(Vector3Int position)
        {
            Debug.Log("Mouse Click position " + position);
            roadManager.PlaceRoad(position);
        }
    }
}

