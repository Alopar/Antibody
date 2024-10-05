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
        
        private int _followerRemains;
        private int _cellFocuserRemains;
        private int _ignorerRemains;
        private int _weakCellsAttackerRemains;

        public WaveSO CurrentWave => _wavesData[_currentWave];

        public int FollowerRemains => _followerRemains;
        public int CellFocuserRemains => _cellFocuserRemains;
        public int IgnorerRemains => _ignorerRemains;
        public int WeakCellsAttackerRemains => _weakCellsAttackerRemains;


        public event Action WaveNumberIncreased;

        private void Awake()
        {
            _instance = this;
        }

        public void NextWave()
        {
            _currentWave++;
            WaveNumberIncreased?.Invoke();
        }
    }
}
