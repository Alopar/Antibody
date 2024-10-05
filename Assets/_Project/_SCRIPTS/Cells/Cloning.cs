using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public enum CloneDirection
    {
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        TopLeft
    }

    [Serializable]
    public struct ClonePoint
    {
        public CloneDirection CloneDirection;
        public int OffsetX;
        public int OffsetY;
        public Transform Point;
    }

    public class Cloning : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private Cell _cell;

        [Space(10)]
        [SerializeField] private List<ClonePoint> _clonePoints;

        [Space(10)]
        [SerializeField] private Cell _cellPrefab;
        #endregion

        #region METHODS PUBLIC
        public void Clone()
        {
            var points = _clonePoints.GetRange(0, 8);
            while (points.Count > 0)
            {
                var point = points[Random.Range(0, points.Count)];
                points.Remove(point);

                var x = _cell.GridIndex.x + point.OffsetX;
                var y = _cell.GridIndex.y + point.OffsetY;
                var cell = _cell.Grid[x, y];
                if (cell is not null) continue;

                cell = Instantiate(_cellPrefab, point.Point.position, Quaternion.identity);
                cell.name = $"Cell({x},{y})";
                cell.SetGridIndex(x, y);

                return;
            }
        }
        #endregion
    }
}
