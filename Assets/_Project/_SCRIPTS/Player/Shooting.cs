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
        [SerializeField] private InputActionReference _switchWeaponAction;

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

        private void SwitchMarker()
        {
            if (!_switchWeaponAction.action.triggered) return;

            _markType += 1;
            _markType = _markType == MarkType.None ? MarkType.X : _markType;
            _marker?.SetMark(_markType);
        }
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            Shoot();
            Reload();
            SwitchMarker();
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
