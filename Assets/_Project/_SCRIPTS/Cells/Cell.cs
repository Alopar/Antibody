using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public enum ViewState
    {
        Strong,
        Normal,
        Sick,
        Damaged,
        Dying,
    }

    [Serializable]
    public class CellView
    {
        public ViewState State;
        public GameObject View;
    }

    public class Cell : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private CellHealth _health;
        [SerializeField] private Cloning _cloning;

        [Space(10)]
        [SerializeField] private List<CellView> _views;
        #endregion

        #region FIELDS PRIVATE
        private static readonly Cell[,] _grid = new Cell[99, 99];
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

        private void UpdateView(ViewState state)
        {
            _views.ForEach(e => e.View.SetActive(false));
            _views.First(e => e.State == state).View.SetActive(true);
        }
        #endregion

        #region HANDLERS
        private void Died()
        {
            _grid[_gridIndex.x, _gridIndex.y] = null;
            Destroy(gameObject);
        }

        private void HealthChange(int current, int max)
        {
            if (_health.IsIncreased)
            {
                UpdateView(ViewState.Strong);
                return;
            }

            if (!_health.IsDamaged)
            {
                UpdateView(ViewState.Normal);
                return;
            }

            var delta = current / max;
            if (delta <= 0.99 && delta > 0.75)
            {
                UpdateView(ViewState.Sick);
                return;
            }
            
            if (delta <= 0.75 && delta > 0.50)
            {
                UpdateView(ViewState.Damaged);
                return;
            }
            
            if (delta <= 0.50)
            {
                UpdateView(ViewState.Dying);
                return;
            }
        }

        private void WaveNumberIncreased()
        {
            if (_health.IsDamaged)
            {
                _health.Repair();
                return;
            }

            if (_cloning.TryClone()) return;
            if (_health.IsIncreased) return;

            _health.IncreaseHealth();
        }
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            _health.OnDied += Died;
            _health.OnHealthChange += HealthChange;
            WavesManager.Instance.WaveNumberIncreased += WaveNumberIncreased;
        }

        private void OnDisable()
        {
            _health.OnDied -= Died;
            _health.OnHealthChange -= HealthChange;
            WavesManager.Instance.WaveNumberIncreased -= WaveNumberIncreased;
        }
        #endregion
    }
}
