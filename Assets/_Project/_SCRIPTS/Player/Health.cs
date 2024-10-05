using System;
using UnityEngine;

namespace Gameplay
{
    public class Health : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(1, 100)] private int _healthMax;
        [SerializeField, Range(0, 10)] private float _invulnerabilityDuration;
        #endregion

        #region FIELDS PRIVATE
        private int _healthCurrent;
        private float _invulnerabilityTimer;
        #endregion

        #region EVENTS
        public event Action<int, int> OnHealthChange;
        public event Action OnDied;
        #endregion

        #region METHODS PUBLIC
        public void DealDamage(int value)
        {
            if (_invulnerabilityTimer > Time.time) return;

            _healthCurrent -= value;
            OnHealthChange?.Invoke(_healthCurrent, _healthMax);

            if (_healthCurrent > 0) return;
            OnDied?.Invoke();
        }
        #endregion
        
        #region METHODS PRIVATE
        private void Init()
        {
            _healthCurrent = _healthMax;
            _invulnerabilityTimer = _invulnerabilityDuration + Time.time;
        }
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            Init();
        }
        #endregion
    }
}
