using System;
using UnityEngine;

namespace Gameplay
{
    public class Cell : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        #endregion

        #region FIELDS PRIVATE
        private static Cell[,] _grid = new Cell[99, 99];

        private (int x, int y) _gridIndex = (50, 50);
        #endregion

        #region PROPERTIES
        public (int x, int y) GridIndex => _gridIndex;
        public Cell[,] Grid => _grid;
        #endregion

        #region METHODS PUBLIC
        public void SetGridIndex(int x, int y)
        {
            _gridIndex = (x, y);
            _grid[x, y] = this;
        }
        #endregion

        #region METHODS PRIVATE
        private void Init()
        {
            SetSelfIndex();
        }

        private void SetSelfIndex()
        {
            if (_gridIndex != (50, 50)) return;
            _grid[_gridIndex.x, GridIndex.y] = this;
        }
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            Init();
        }
        #endregion
    }
}
