using System;
using UnityEngine;

namespace Gameplay
{
    public class LookAtPointer : MonoBehaviour
    {
        #region FIELDS PRIVATE
        private Camera _cameraCache;
        private Vector3 _pointerPosition;
        #endregion

        #region PROPERTIES
        public Vector3 PointerPosition => _pointerPosition;
        #endregion

        #region METHODS PRIVATE
        private void LookAtMousePosition()
        {
            _pointerPosition = _cameraCache.ScreenToWorldPoint(Input.mousePosition);
            _pointerPosition.z = transform.position.z;

            var direction = _pointerPosition - transform.position;
            var scale = transform.localScale;
            scale.x = direction.x < 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            _cameraCache = Camera.main;
        }

        private void Update()
        {
            LookAtMousePosition();
        }
        #endregion
    }
}
