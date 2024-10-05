using System;
using UnityEngine;

namespace Gameplay
{
    public class CellHealth : Health
    {
        #region PROPERTIES
        public bool IsDamaged => _healthCurrent < _healthMax;
        public bool IsIncreased => _healthCurrent > _healthMax;
        #endregion

        #region METHODS PUBLIC
        public void Repair()
        {
            SetHealth(_healthMax);
        }

        public void IncreaseHealth()
        {
            SetHealth(_healthMax * 2);
        }
        #endregion
    }
}
