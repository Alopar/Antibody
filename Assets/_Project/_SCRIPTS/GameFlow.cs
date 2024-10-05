using UnityEngine;

namespace Gameplay
{
    public class GameFlow : MonoBehaviour
    {
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
