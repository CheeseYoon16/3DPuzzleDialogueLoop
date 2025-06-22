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

        private Dictionary<Vector3Int, StructureModel> tmpRoadObjects = new Dictionary<Vector3Int, StructureModel>();

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

        internal void PlaceTemporaryStructure(Vector3Int position, GameObject structPrefab, CellType cellType)
        {
            placementGrid[position.x, position.z] = cellType;

            StructureModel structure = CreateNewStructureModel(position, structPrefab, cellType);

            if(!tmpRoadObjects.ContainsKey(position))
            {
                tmpRoadObjects.Add(position, structure);
            }
        }

        private StructureModel CreateNewStructureModel(Vector3Int position, GameObject structPrefab, CellType cellType)
        {
            GameObject structure = new GameObject(cellType.ToString());
            structure.transform.SetParent(transform);
            structure.transform.localPosition = position;
            var structureModel = structure.AddComponent<StructureModel>();

            structureModel.CreateModel(structPrefab);

            return structureModel;
        }

        public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
        {
            if(tmpRoadObjects.ContainsKey(position))
            {
                tmpRoadObjects[position].SwapModel(newModel, rotation);
            }
        }

        internal CellType[] GetNeighbourTypesFor(Vector3Int position)
        {
            return placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
        }

        internal List<Vector3Int> GetNeighboursOfTypesFor(Vector3Int position, CellType celltype)
        {
            var neighborVertices = placementGrid.GetAdjacentCellsOfType(position.x, position.z, celltype);
            List<Vector3Int> neighbors = new List<Vector3Int>();
            foreach(var point in neighborVertices)
            {
                neighbors.Add(new Vector3Int(point.X, 0, point.Y));
            }

            return neighbors;
        }
    }
}

