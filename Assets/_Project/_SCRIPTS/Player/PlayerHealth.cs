using System;
using UnityEngine;

namespace Gameplay
{
    public class PlayerHealth : Health
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _invulnerabilityDuration;
        #endregion

        #region FIELDS PRIVATE
        private float _invulnerabilityTimer;
        #endregion

        #region METHODS PUBLIC
        public override void DealDamage(int value)
        {
            if (_invulnerabilityTimer > Time.time) return;
            base.DealDamage(value);
        }

        public void Resurrection()
        {
            SetHealth(_healthMax);
            _invulnerabilityTimer = _invulnerabilityDuration + Time.time;
        }
        #endregion
    }
}
