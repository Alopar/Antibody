using UnityEngine;

namespace Gameplay
{
    public class GameFlow : MonoBehaviour
    {
        #region SINGLETONE
        private static GameFlow _instance;
        public static GameFlow Instance => _instance;
        #endregion

        public static bool ShowTutorial = true;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            EnemyManager.Instance.EnemyKilled += OnEnemyKilled;
            Cell.CellDied += OnCellDie;
        }

        private void OnCellDie(Cell cell)
        {
            if (Cell.CellsCount <= 0)
                Lose();
        }

        private void OnEnemyKilled(Enemy enemy)
        {
            if (EnemyManager.Instance.EnemiesCount == 0)
            {
                WavesManager.Instance.NextWave();
            }
        }

        public void Lose()
        {
            Pauser.Instance.Pause();
            UIManager.Instance.DefeatMenu.SetActive(true);
        }
    }
}
