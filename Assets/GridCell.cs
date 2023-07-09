using UnityEngine;

namespace DefaultNamespace
{
    public class GridCell : MonoBehaviour
    {
        public int     RowIndex, ColumnIndex;
        public Vector3 Position;
        public bool    IsPopulated = false;
    }
}