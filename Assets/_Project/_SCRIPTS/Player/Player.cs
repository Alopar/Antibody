using System;
using UnityEngine;

namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private Health _health;
        #endregion

        #region FIELDS PRIVATE
        #endregion

        #region METHODS PUBLIC
        #endregion

        #region METHODS PRIVATE
        private void Init()
        {
            
        }
        #endregion

        #region HANDLERS
        private void Died()
        {
            Destroy(gameObject);
        }
        #endregion

        #region UNITY CALLBACKS
        private void OnEnable()
        {
            _health.OnDied += Died;
        }

        private void OnDisable()
        {
            _health.OnDied -= Died;
        }
        #endregion

        
    }
}
