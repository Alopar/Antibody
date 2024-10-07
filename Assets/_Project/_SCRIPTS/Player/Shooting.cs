using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class Shooting : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _shootCooldown = 0.5f;

        [Space(10)]
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Marker _projectilePrefab;

        [Space(10)]
        [SerializeField] private InputActionReference _shootAction;
        [SerializeField] private InputActionReference _nextWeaponAction;
        [SerializeField] private InputActionReference _previousWeaponAction;

        [Space(10)]
        [SerializeField] private LookAtPointer _look;
        #endregion

        #region FIELDS PRIVATE
        private float _cooldownTimer;
        private Marker _marker;
        private MarkType _markType;
        private bool _isShoot;
        #endregion

        #region PROPERTIES
        public bool IsShoot => _isShoot;
        public MarkType Mark => _markType;
        #endregion

        #region EVENTS
        public event Action<MarkType> SwitchedMark;
        #endregion

        #region METHODS PRIVATE
        private void Reload()
        {
            if (_marker) return;
            if (_cooldownTimer > Time.time) return;

            _marker = Instantiate(_projectilePrefab, _shootPoint);
            _marker.SetMark(_markType);
        }

        private void Shoot()
        {
            _isShoot = false;
            if (!_marker) return;
            if (!_shootAction.action.IsPressed()) return;

            _isShoot = true;
            var direction = _look.PointerPosition - _shootPoint.position;
            _marker.transform.right = direction;
            _marker.transform.SetParent(null);
            _marker.Throw();
            _marker = null;

            _cooldownTimer = _shootCooldown + Time.time;
        }

        private void NextMarker()
        {
            if (!_nextWeaponAction.action.triggered) return;

            _markType = _markType == MarkType.N ? MarkType.X : _markType + 1;
            _marker?.SetMark(_markType);
            SwitchedMark?.Invoke(_markType);
        }
        
        private void PreviousMarker()
        {
            if (!_previousWeaponAction.action.triggered) return;

            _markType = _markType == MarkType.X ? MarkType.N : _markType - 1;
            _marker?.SetMark(_markType);
            SwitchedMark?.Invoke(_markType);
        }
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            Shoot();
            Reload();
            NextMarker();
            PreviousMarker();
        }

        private void OnDisable()
        {
            if(!_marker) return;

            Destroy(_marker.gameObject);
            _marker = null;
        }
        #endregion
    }
}
