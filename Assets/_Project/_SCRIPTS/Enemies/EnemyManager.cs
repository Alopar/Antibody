using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EnemyManager : MonoBehaviour
    {
        #region SINGLETON
        private static EnemyManager _instance;
        public static EnemyManager Instance => _instance;
        #endregion

        [SerializeField] private FollowPlayerEnemy _followPlayerEnemyPrefab;
        [SerializeField] private PriorityCellEnemy _priorityCellEnemyPrefab;
        [SerializeField] private Player _player;
        [SerializeField] private CellTemp _cell;

        private List<Enemy> _enemies = new();
        
        public int EnemiesCount => _enemies.Count;


        private void Awake()
        {
            _instance = this;
        }

        public void SpawnEnemy<T>(Vector3 position) where T : Enemy 
        {
            Enemy enemyToSpawn = null;

            if (typeof(T) == _followPlayerEnemyPrefab.GetType())
            {
                enemyToSpawn = _followPlayerEnemyPrefab;
                WavesManager.Instance.DecreaseFollowersToSpawn();
            }
            else if (typeof(T) == _priorityCellEnemyPrefab.GetType())
            {
                enemyToSpawn = _priorityCellEnemyPrefab;
                WavesManager.Instance.DecreaseCellFocusersToSpawn();
            }
            else
                Debug.LogError($"Enemy of type {typeof(T)} not found");

            var enemy = Instantiate(enemyToSpawn, position, Quaternion.identity);
            enemy.Init(_player, _cell);
            _enemies.Add(enemy);
        }
    }
}
