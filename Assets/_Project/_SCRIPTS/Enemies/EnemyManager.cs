﻿using System;
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
        [SerializeField] private IgnorerEnemy _ignorerEnemyPrefab;
        [SerializeField] private WeakCellFocuserEnemy _weakCellFocuserEnemyPrefab;
        [SerializeField] private Player _player;
        [SerializeField] private Cell _cell;

        private List<Enemy> _enemies = new();
        
        public int EnemiesCount => _enemies.Count;

        public event Action<Enemy> EnemyMarked;

        public event Action<Enemy> EnemyKilled;

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
            else if (typeof(T) == _ignorerEnemyPrefab.GetType())
            {
                enemyToSpawn = _ignorerEnemyPrefab;
                WavesManager.Instance.DecreaseIgnorersToSpawn();
            }
            else if (typeof(T) == _weakCellFocuserEnemyPrefab.GetType())
            {
                enemyToSpawn = _weakCellFocuserEnemyPrefab;
                WavesManager.Instance.DecreaseWeakCellsAttackersToSpawn();
            }
            else
                Debug.LogError($"Enemy of type {typeof(T)} not found");

            var enemy = Instantiate(enemyToSpawn, position, Quaternion.identity);

            enemy.Init(_player);
            _enemies.Add(enemy);
        }

        public void Kill(Enemy enemy)
        {
            _enemies.Remove(enemy);
            enemy.Die();
            EnemyKilled?.Invoke(enemy);
        }

        public void TriggerEnemyMarked(Enemy enemy)
        {
            EnemyMarked?.Invoke(enemy);
        }

        public void DisableEnemies()
        {
            foreach (var enemy in _enemies)
                enemy.enabled = false;
        }
    }
}
