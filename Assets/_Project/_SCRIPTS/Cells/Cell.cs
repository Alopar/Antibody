using Spine.Unity;
using UnityEngine;

namespace Gameplay
{
    [SelectionBase]
    public class Cell : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private CellHealth _health;
        [SerializeField] private Cloning _cloning;

        [Space(10)]
        [SerializeField] private SkeletonAnimation _skeleton;
        #endregion

        #region FIELDS PRIVATE
        private static readonly Cell[,] _grid = new Cell[99, 99];
        private (int x, int y) _gridIndex = (50, 50);
        private static int _cellsCount;
        #endregion

        #region PROPERTIES
        public (int x, int y) GridIndex => _gridIndex;
        public Cell[,] Grid => _grid;
        public static int CellsCount => _cellsCount;
        #endregion

        #region METHODS PUBLIC
        public void SetGridIndex(int x, int y)
        {
            _gridIndex = (x, y);
            _grid[x, y] = this;
        }
        #endregion

        #region EVENTS
        public static event System.Action<Cell> CellDied;
        #endregion

        #region METHODS PRIVATE
        private void Init()
        {
            SetSelfIndex();
            UpdateView("full");
            _cellsCount++;
        }

        private void SetSelfIndex()
        {
            if (_gridIndex != (50, 50)) return;
            _grid[_gridIndex.x, GridIndex.y] = this;
        }

        private void UpdateView(string skin)
        {
            _skeleton.Skeleton.SetSkin(skin);
            _skeleton.Skeleton.SetSlotsToSetupPose();
            _skeleton.LateUpdate();
        }
        #endregion

        #region HANDLERS
        private void Died()
        {
            _grid[_gridIndex.x, _gridIndex.y] = null;
            Destroy(gameObject);
            _cellsCount--;
            CellDied?.Invoke(this);
        }

        private void HealthChange(int current, int max)
        {
            if (_health.IsIncreased)
            {
                UpdateView("super");
                return;
            }

            if (!_health.IsDamaged)
            {
                UpdateView("full");
                return;
            }

            var delta = (float)current / max;
            if (delta <= 0.99 && delta > 0.75)
            {
                UpdateView("damaged");
                return;
            }
            
            if (delta <= 0.75 && delta > 0.50)
            {
                UpdateView("half");
                return;
            }
            
            if (delta <= 0.50)
            {
                UpdateView("third");
                return;
            }
        }

        private void RoundEnded()
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
            GameFlow.Instance.RoundEnded += RoundEnded;
        }

        private void OnDisable()
        {
            _health.OnDied -= Died;
            _health.OnHealthChange -= HealthChange;
            GameFlow.Instance.RoundEnded -= RoundEnded;
        }
        #endregion
    }
}
