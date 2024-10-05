using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private PlayerHealth _health;
        #endregion

        #region HANDLERS
        private void Died()
        {
            var cells = FindObjectsByType<Cell>(FindObjectsSortMode.None);
            var cell = cells[Random.Range(0, cells.Length)];

            transform.position = cell.transform.position;
            _health.Resurrection();
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
