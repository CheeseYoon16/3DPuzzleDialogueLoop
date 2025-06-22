using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace TuruSore.BuildMode
{
    public class RoadManager : MonoBehaviour
    {
        public PlacementManager placementManager;

        public List<Vector3Int> tmpPlacementPost = new List<Vector3Int>();

        public GameObject roadStraight;

        public void PlaceRoad(Vector3Int position)
        {
            if(placementManager!=null)
            {
                if(!placementManager.CheckIfPositionInBound(position))
                {
                    return;
                }

                if(!placementManager.CheckIfPositionIsFree(position))
                {
                    return;
                }

                placementManager.PlaceTemporaryStructure(position, roadStraight, CellType.Road);
            }

        }
    }
}

