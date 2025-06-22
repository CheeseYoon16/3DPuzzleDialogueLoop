using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TuruSore.BuildMode
{
    public class PlacementManager : MonoBehaviour
    {
        public int width, height;
        Grid placementGrid;

        private void OnEnable()
        {
            placementGrid = new Grid(width, height);
        }

        internal bool CheckIfPositionInBound(Vector3Int position)
        {
            if(position.x >= 0 && position.x < width && position.z >= 0 && position.z < height)
            {
                return true;
            }

            return false;
        }

        internal bool CheckIfPositionIsFree(Vector3Int position)
        {
            return CheckIfPositionOfType(position, CellType.Empty);
        }

        private bool CheckIfPositionOfType(Vector3Int position, CellType celltype)
        {
            return placementGrid[position.x, position.z] == celltype;
        }

        internal void PlaceTemporaryStructure(Vector3Int position, GameObject roadStraight, CellType cellType)
        {
            placementGrid[position.x, position.z] = cellType;

            GameObject newStruct = Instantiate(roadStraight, position, Quaternion.identity);
        }
    }
}

