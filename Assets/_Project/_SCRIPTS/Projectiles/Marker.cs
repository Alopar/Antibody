using System;
using UnityEngine;

namespace Gameplay
{
    public class Marker : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 99)] private float _speed = 1;

        [Space(10)]
        [SerializeField] private Sprite _x;
        [SerializeField] private Sprite _y;
        [SerializeField] private Sprite _n;

        [Space(10)]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        #endregion

        #region FIELDS PRIVATE
        private MarkType _markType;
        #endregion
        
        #region METHODS PUBLIC
        public void SetMark(MarkType markType)
        {
            _markType = markType;
            switch (_markType)
            {
                case MarkType.X:
                    _renderer.sprite = _x;
                    break;
                case MarkType.Y:
                    _renderer.sprite = _y;
                    break;
                case MarkType.N:
                    _renderer.sprite = _n;
                    break;
            }
        }

        public void Throw()
        {
            _rigidbody2D.simulated = true;
            _rigidbody2D.AddForce(transform.right * _speed, ForceMode2D.Impulse);
            _animator.SetTrigger("IsFly");
            Invoke(nameof(Die), 3f);
        }
        #endregion

        #region METHODS PRIVATE
        private void Die()
        {
            _rigidbody2D.simulated = false;
            _animator.SetTrigger("IsDie");
            Destroy(gameObject, 0.5f);
        }
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            _rigidbody2D.simulated = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent<Enemy>(out var component)) return;

            component.Mark(_markType);
            Die();
        }
        #endregion
    }
}
