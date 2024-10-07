using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class WavesManager : MonoBehaviour
    {
        #region SINGLETON
        private static WavesManager _instance;
        public static WavesManager Instance => _instance;
        #endregion

        [SerializeField] private WaveSO[] _wavesData;

        private int _currentWave = 0;
        
        private int _followersToSpawn;
        private int _cellFocusersToSpawn;
        private int _ignorersToSpawn;
        private int _weakCellsAttackersToSpawn;

        private WaveSO _outOfRangeWave;

        public WaveSO CurrentWave 
        {
            get
            {
                if (_currentWave < _wavesData.Length)
                    return _wavesData[_currentWave];

                return _outOfRangeWave;
            }
        }

        public int CurrentWaveIndex => _currentWave;

        public int FollowersToSpawn => _followersToSpawn;
        public int CellFocusersToSpawn => _cellFocusersToSpawn;
        public int IgnorersToSpawn => _ignorersToSpawn;
        public int WeakCellsAttackersToSpawn => _weakCellsAttackersToSpawn;

        public int RemainingEnemiesToSpawn => _followersToSpawn + _cellFocusersToSpawn + _ignorersToSpawn + _weakCellsAttackersToSpawn;

        public event Action WaveNumberIncreased;

        private void Awake()
        {
            _instance = this;
            ResetEnemiesToSpawnCount();
        }

        public void NextWave()
        {
            _currentWave++;
            if (_currentWave >= _wavesData.Length)
                _outOfRangeWave = GetWaveOutOfRange();

            ResetEnemiesToSpawnCount();
            WaveNumberIncreased?.Invoke();
        }

        private void ResetEnemiesToSpawnCount()
        {
            _followersToSpawn = CurrentWave.FollowPlayerEnemyCount;
            _cellFocusersToSpawn = CurrentWave.PrioritiseCellEnemyCount;
            _ignorersToSpawn = CurrentWave.IgnorePlayerEnemyCount;
            _weakCellsAttackersToSpawn = CurrentWave.FocusWeakCellEnemyCount;
        }

        public void DecreaseFollowersToSpawn()
        {
            _followersToSpawn--;
        }
        public void DecreaseCellFocusersToSpawn()
        {
            _cellFocusersToSpawn--;
        }
        public void DecreaseIgnorersToSpawn()
        {
            _ignorersToSpawn--;
        }
        public void DecreaseWeakCellsAttackersToSpawn()
        {
            _weakCellsAttackersToSpawn--;
        }

        private WaveSO GetWaveOutOfRange()
        {
            int increase = _currentWave - _wavesData.Length + 1;

            return _wavesData[_wavesData.Length - 1].CreateCopy(increase); ;
        }
    }
}
