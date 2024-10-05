using System;
using UnityEngine;

namespace Gameplay
{
    public class Marker : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _speed = 1;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        #endregion

        #region FIELDS PRIVATE
        private MarkType _markType;
        #endregion
        
        #region METHODS PUBLIC
        public void SetMark(MarkType markType)
        {
            _markType = markType;
        }
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            _rigidbody2D.AddForce(transform.right * _speed, ForceMode2D.Impulse);
            Destroy(gameObject, 3f);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent<Enemy>(out var component)) return;

            component.Mark(_markType);
            Destroy(gameObject);
        }
        #endregion
    }
}
