using System;
using UnityEngine;

namespace Gameplay
{
    public class LookAtPointer : MonoBehaviour
    {
        #region FIELDS PRIVATE
        private Camera _cameraCache;
        #endregion
        
        #region METHODS PRIVATE
        private void LookAtMousePosition()
        {
            var pointerPoint = _cameraCache.ScreenToWorldPoint(Input.mousePosition);
            pointerPoint.z = transform.position.z;
            
            var direction = pointerPoint - transform.position;
            transform.right = direction.normalized;
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
