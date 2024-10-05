﻿using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Wave")]
    public class WaveSO : ScriptableObject
    {
        [SerializeField] private float _spawnCooldown;
        [SerializeField] private int _followPlayerEnemyCount;
        [SerializeField] private int _prioritiseCellEnemyCount;
        [SerializeField] private int _focusWeakCellEnemyCount;
        [SerializeField] private int _ignorePlayerEnemyCount;
        [SerializeField, Range(1, 3)] private int _allowedMarks = 3;

        public float SpawnCooldown => _spawnCooldown;
        public int AllowedMarks => _allowedMarks;
        public int FollowPlayerEnemyCount => _followPlayerEnemyCount;
        public int PrioritiseCellEnemyCount => _prioritiseCellEnemyCount;
        public int FocusWeakCellEnemyCount => _focusWeakCellEnemyCount;
        public int IgnorePlayerEnemyCount => _ignorePlayerEnemyCount;
        public int EnemiesCount => _ignorePlayerEnemyCount + _focusWeakCellEnemyCount + _prioritiseCellEnemyCount + _followPlayerEnemyCount;
    }
}
