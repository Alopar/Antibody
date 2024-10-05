using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class EnemiesKilledText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private bool _initialized;
        private int _killedEnemies;

        private void Start()
        {
            _killedEnemies = 0;
            if (!_initialized)
            {
                EnemyManager.Instance.EnemyKilled += OnEnemyKilled;
                _text.text = "Enemies: " + _killedEnemies + "/" + WavesManager.Instance.CurrentWave.EnemiesCount.ToString();
                _initialized = true;
                WavesManager.Instance.WaveNumberIncreased += OnNewWave;
            }
        }

        private void OnNewWave()
        {
            _killedEnemies = 0;
            _text.text = "Enemies: " + _killedEnemies + "/" + WavesManager.Instance.CurrentWave.EnemiesCount.ToString();
        }

        private void OnEnemyKilled(Enemy enemy)
        {
            _killedEnemies++;
            _text.text = "Enemies: " + _killedEnemies + "/" + WavesManager.Instance.CurrentWave.EnemiesCount.ToString();
        }

        private void OnEnable()
        {
            if (WavesManager.Instance == null)
                return;
            _text.text = "Enemies: " + _killedEnemies + "/" + WavesManager.Instance.CurrentWave.EnemiesCount.ToString();
            EnemyManager.Instance.EnemyKilled += OnEnemyKilled;
            WavesManager.Instance.WaveNumberIncreased += OnNewWave;
            _initialized = true;
        }

        private void OnDisable()
        {
            WavesManager.Instance.WaveNumberIncreased -= OnNewWave;
            EnemyManager.Instance.EnemyKilled -= OnEnemyKilled;
        }
    }
}
