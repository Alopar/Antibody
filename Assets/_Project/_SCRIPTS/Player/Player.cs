using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    [SelectionBase]
    public class Player : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _resurrectionDelay;
        
        [Space(10)]
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private Shooting _shooting;
        [SerializeField] private Movement _movement;
        [SerializeField] private LookAtPointer _lookAtPointer;
        #endregion

        #region FIELDS PRIVATE
        private bool _isDie;
        #endregion

        #region PROPERTIES
        public bool IsDie => _isDie;
        #endregion

        #region HANDLERS
        private void Init()
        {
            TurnOn();
        }
        
        private void TurnOn()
        {
            _shooting.enabled = true;
            _movement.enabled = true;
            _lookAtPointer.enabled = true;
        }
        
        private void TurnOff()
        {
            _shooting.enabled = false;
            _movement.enabled = false;
            _lookAtPointer.enabled = false;
        }

        private void Died()
        {
            TurnOff();
            _isDie = true;

            Invoke(nameof(Resurrection), _resurrectionDelay);
        }

        private void Resurrection()
        {
            TurnOn();
            _isDie = false;

            var cells = FindObjectsByType<Cell>(FindObjectsSortMode.None);
            var cell = cells[Random.Range(0, cells.Length)];

            transform.position = cell.transform.position;
            _health.Resurrection();
        }
        #endregion

        #region UNITY CALLBACKS

        private void Awake()
        {
            Init();
        }

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
