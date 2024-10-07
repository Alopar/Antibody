using System.Collections;
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

        public event System.Action RoundEnded;

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
                StartCoroutine(EndRound());
            }
        }

        public void Lose()
        {
            Pauser.Instance.Pause();
            UIManager.Instance.DefeatMenu.SetActive(true);
        }

        private IEnumerator EndRound()
        {
            yield return new WaitForSeconds(1);
            RoundEnded?.Invoke();

            yield return new WaitForSeconds(1.8f);
            WavesManager.Instance.NextWave();
        }

        private void OnDestroy()
        {
            Cell.CellDied -= OnCellDie;
        }
    }
}
