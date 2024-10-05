using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class Movement : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _speed = 1f;
        [SerializeField] private InputActionReference _moveAction;
        #endregion

        #region METHODS PRIVATE
        private void Move()
        {
            var moveValue = _moveAction.action.ReadValue<Vector2>();
            var direction = new Vector3(moveValue.x, moveValue.y, 0);
            transform.position += direction * _speed * Time.deltaTime;
        }
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            Move();
        }
        #endregion
    }
}
