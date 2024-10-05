using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class Shooting : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _shootCooldown = 0.5f;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Marker _projectilePrefab;
        
        [Space(10)]
        [SerializeField] private InputActionReference _shootAction;
        
        [Space(10)]
        [SerializeField] private MarkType _markType;
        #endregion

        #region FIELDS PRIVATE
        private float _cooldownTimer;
        #endregion

        #region METHODS PRIVATE
        private void Shoot()
        {
            if (_cooldownTimer > Time.time) return;
            if (!_shootAction.action.IsPressed()) return;

            _cooldownTimer = _shootCooldown + Time.time;
            var projectile = Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation);
            projectile.SetMark(_markType);
        }
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            Shoot();
        }
        #endregion
    }
}
