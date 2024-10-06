using UnityEngine;

namespace Gameplay
{
    public class GameFlow : MonoBehaviour
    {
        #region SINGLETONE
        private static GameFlow _instance;
        public static GameFlow Instance => _instance;
        #endregion

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            EnemyManager.Instance.EnemyKilled += OnEnemyKilled;
        }

        private void OnEnemyKilled(Enemy enemy)
        {
            if (EnemyManager.Instance.EnemiesCount == 0)
            {
                WavesManager.Instance.NextWave();
            }
        }
    }
}
