using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TuruSore.BuildMode
{
    public class RoadFixer : MonoBehaviour
    {
        public GameObject deadEnd, roadStraight, corner, threeway, fourway;

        public void FixRoadAtPosition(PlacementManager placementManager, Vector3Int tmpPosition)
        {
            //[right, up, left, down]
            var result = placementManager.GetNeighbourTypesFor(tmpPosition);

            int roadCount = 0;
            roadCount = result.Where(x => x == CellType.Road).Count();

            if(roadCount == 0 || roadCount == 1)
            {
                CreateDeadEnd(placementManager, result,  tmpPosition);
            }
            else if(roadCount == 2)
            {
                if(CreateStraightRoad(placementManager, result, tmpPosition))
                {
                    return;
                }

                CreateCorner(placementManager, result, tmpPosition);
            }
            else if(roadCount == 3)
            {
                Create3Way(placementManager, result, tmpPosition);
            }
            else
            {
                Create4Way(placementManager, result, tmpPosition);
            }
        }

        private void Create4Way(PlacementManager placementManager, CellType[] result, Vector3Int tmpPosition)
        {
            placementManager.ModifyStructureModel(tmpPosition, fourway, Quaternion.identity);
        }


        private void Create3Way(PlacementManager placementManager, CellType[] result, Vector3Int tmpPosition)
        {
            //sequence [left,up, right, down]

            if (result[1] == CellType.Road && result[2] == CellType.Road && result[3] == CellType.Road) //up, right, down => default state in the prefab
            {
                placementManager.ModifyStructureModel(tmpPosition, threeway, Quaternion.identity);
            }
            else if (result[2] == CellType.Road && result[3] == CellType.Road && result[0] == CellType.Road) //right,down, left => rotate 90 degree
            {
                placementManager.ModifyStructureModel(tmpPosition, threeway, Quaternion.Euler(0, 90, 0));
            }
            else if (result[3] == CellType.Road && result[0] == CellType.Road && result[1] == CellType.Road) //down, left, up => rotate 180 degree
            {
                placementManager.ModifyStructureModel(tmpPosition, threeway, Quaternion.Euler(0, 180, 0));
            }
            else if (result[0] == CellType.Road && result[1] == CellType.Road && result[2] == CellType.Road) //left, up, right => rotate 270 degree
            { 
                placementManager.ModifyStructureModel(tmpPosition, threeway, Quaternion.Euler(0, 270, 0));
            }

        }

        private void CreateCorner(PlacementManager placementManager, CellType[] result, Vector3Int tmpPosition)
        {
            //sequence [left,up, right, down] => doesnt care any particular order in corner

            if (result[1] == CellType.Road && result[2] == CellType.Road) //up, right
            {
                placementManager.ModifyStructureModel(tmpPosition, corner, Quaternion.Euler(0,90,0));
            }
            else if (result[2] == CellType.Road && result[3] == CellType.Road) //right,down
            {
                placementManager.ModifyStructureModel(tmpPosition, corner, Quaternion.Euler(0, 180, 0));
            }
            else if (result[3] == CellType.Road && result[0] == CellType.Road) //down, left
            {
                placementManager.ModifyStructureModel(tmpPosition, corner, Quaternion.Euler(0, 270, 0));
            }
            else if (result[0] == CellType.Road && result[1] == CellType.Road) //left, up => default in prefab
            {
                placementManager.ModifyStructureModel(tmpPosition, corner, Quaternion.identity);
            }

        }

        private bool CreateStraightRoad(PlacementManager placementManager, CellType[] result, Vector3Int tmpPosition)
        {
            //straight only care if left & right, up & down in no particular order
            if (result[0] == CellType.Road && result[2] == CellType.Road)
            {
                placementManager.ModifyStructureModel(tmpPosition, roadStraight, Quaternion.identity);
                return true;
            }
            else if (result[1] == CellType.Road && result[3] == CellType.Road)
            {
                placementManager.ModifyStructureModel(tmpPosition, roadStraight, Quaternion.Euler(0,90,0));
                return true;
            }

            return false;
        }

        private void CreateDeadEnd(PlacementManager placementManager, CellType[] result, Vector3Int tmpPosition)
        {
            //sequence [left,up, right, down] => check only if one connected road exist

            if (result[1] == CellType.Road) //up
            {
                placementManager.ModifyStructureModel(tmpPosition, deadEnd, Quaternion.Euler(0, 270, 0));
            }
            else if (result[2] == CellType.Road) //right
            {
                placementManager.ModifyStructureModel(tmpPosition, deadEnd, Quaternion.identity);
            }
            else if (result[3] == CellType.Road) //down
            {
                placementManager.ModifyStructureModel(tmpPosition, deadEnd, Quaternion.Euler(0, 90, 0));
            }
            else if (result[0] == CellType.Road) //left
            {
                placementManager.ModifyStructureModel(tmpPosition, deadEnd, Quaternion.Euler(0, 180, 0));
            }
        }
    }
}

