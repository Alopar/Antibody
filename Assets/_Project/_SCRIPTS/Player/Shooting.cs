using System;
using TMPro;
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
        [SerializeField] private TextMeshPro _markerText;
        #endregion

        #region FIELDS PRIVATE
        private float _cooldownTimer;
        private MarkType _markType;
        #endregion

        #region METHODS PRIVATE
        private void Init()
        {
            _markerText.text = _markType.ToString();
        }

        private void Shoot()
        {
            if (_cooldownTimer > Time.time) return;
            if (!_shootAction.action.IsPressed()) return;

            _cooldownTimer = _shootCooldown + Time.time;
            var projectile = Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation);
            projectile.SetMark(_markType);
        }

        private void SwitchMarker()
        {
            if (!_switchWeaponAction.action.triggered) return;

            _markType += 1;
            _markType = _markType == MarkType.None ? MarkType.X : _markType;
            _markerText.text = _markType.ToString();
        }
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            Shoot();
            SwitchMarker();
        }
        #endregion
    }
}
