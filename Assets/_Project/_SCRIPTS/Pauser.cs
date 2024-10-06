using UnityEngine;
using System;

namespace Gameplay
{
    public class Pauser : MonoBehaviour
    {
        #region SINGLETONE
        private static Pauser _instance;
        public static Pauser Instance => _instance;
        #endregion

        public bool Paused { get; private set; }
        
        public event Action GamePaused;
        public event Action GameResumed;

        private void Awake()
        {
            _instance = this;
        }

        public void Pause()
        {
            Time.timeScale = 0;
            Paused = true;
            GamePaused?.Invoke();
        }

        public void Resume()
        {
            Time.timeScale = 1;
            Paused = false;
            GameResumed?.Invoke();
        }
    }
}