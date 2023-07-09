using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace
{
    public class Grid : MonoBehaviour
    {
        public int        Columns = 6;
        public int        Rows    = 13;
        public float      Offset  = 0.5f;
        public GameObject GridCellPrefab;

        public int              InitialGridFill = 40;
        public List<GameObject> BallPrefabs;

        private List<GridCell> _gridCells;

        public void GenerateGrid()
        {
            _gridCells = new();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GameObject obj = Instantiate(GridCellPrefab, transform);
                    obj.transform.localPosition = new Vector3(j * Offset, -i * Offset);
                    GridCell gridCell = obj.AddComponent<GridCell>();
                    gridCell.RowIndex    = i;
                    gridCell.ColumnIndex = j;
                    gridCell.Position    = obj.transform.localPosition;
                    _gridCells.Add(gridCell);
                }
            }
        }

        public void EmptyGridBalls()
        {
            if (transform.childCount == 0) return;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void FillGridWithBalls()
        {
            for (int i = 0; i < InitialGridFill; i++)
            {
                SpawnBallOnGrid(_gridCells);
            }
        }

        public void SpawnBallOnGrid(List<GridCell> gridCells)
        {
            GridCell randomGridCell = gridCells[UnityEngine.Random.Range(0, InitialGridFill)];
            if (randomGridCell.IsPopulated)
            {
                SpawnBallOnGrid(gridCells);
            }
            else
            {
                GameObject spawnedBall = Instantiate(BallPrefabs[UnityEngine.Random.Range(0, BallPrefabs.Count - 1)], transform);
                spawnedBall.transform.localPosition = randomGridCell.Position;
                randomGridCell.IsPopulated          = true;
            }
        }

        public GameObject GetBall()
        {
            return Instantiate(BallPrefabs[UnityEngine.Random.Range(0, BallPrefabs.Count)]);
        }
    }
}