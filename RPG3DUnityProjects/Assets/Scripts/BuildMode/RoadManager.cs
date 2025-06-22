using System;
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
        public List<Vector3Int> roadPositionsRecheckList = new List<Vector3Int>();

        public GameObject roadStraight;
        public RoadFixer roadFixer;

        private void OnEnable()
        {
            if(roadFixer == null)
            {
                roadFixer = GetComponent<RoadFixer>();
            }
        }

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

                tmpPlacementPost.Clear();
                tmpPlacementPost.Add(position);

                placementManager.PlaceTemporaryStructure(position, roadStraight, CellType.Road);

                FixRoadPrefab();
            }

        }

        private void FixRoadPrefab()
        {
            if(roadFixer!=null)
            {
                foreach (var tmpPos in tmpPlacementPost)
                {
                    roadFixer.FixRoadAtPosition(placementManager, tmpPos);
                    var neighbors = placementManager.GetNeighboursOfTypesFor(tmpPos, CellType.Road);

                    foreach(var roadPos in neighbors)
                    {
                        if(!roadPositionsRecheckList.Contains(roadPos))
                        {
                            roadPositionsRecheckList.Add(roadPos);
                        }
                    }
                }

                foreach(var positionToFix in roadPositionsRecheckList)
                {
                    roadFixer.FixRoadAtPosition(placementManager, positionToFix);
                }
            }

        }
    }
}

