using System;
using UnityEngine;

namespace Gameplay
{
    public class Health : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(1, 100)] protected int _healthMax;
        #endregion

        #region FIELDS PRIVATE
        protected int _healthCurrent;
        #endregion

        #region EVENTS
        public event Action<int, int> OnHealthChange;
        public event Action OnDied;
        #endregion

        #region METHODS PUBLIC
        public virtual void DealDamage(int value)
        {
            ChangeHealth(-value);

            if (_healthCurrent > 0) return;
            OnDied?.Invoke();
        }
        #endregion
        
        #region METHODS PRIVATE
        protected virtual void Init()
        {
            SetHealth(_healthMax);
        }

        private void ChangeHealth(int value)
        {
            SetHealth(_healthCurrent + value);
        }

        protected void SetHealth(int value)
        {
            _healthCurrent = value;
            OnHealthChange?.Invoke(_healthCurrent, _healthMax);
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
