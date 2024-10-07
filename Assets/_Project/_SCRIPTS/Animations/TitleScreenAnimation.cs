using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class TitleScreenAnimation : MonoBehaviour
    {
        [SerializeField] private Image _logo;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _rotationAngle;
        [SerializeField] private float _back1Speed;
        [SerializeField] private float _back2Speed;
        [SerializeField] private float _back3Speed;
        [SerializeField] private float _backMoveRange;
        [SerializeField] private RectTransform _backBubbles1;
        [SerializeField] private RectTransform _backBubbles2;
        [SerializeField] private RectTransform _middleBubbles1;
        [SerializeField] private RectTransform _middleBubbles2;
        [SerializeField] private RectTransform _forwardBack;

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;
            _logo.transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(_timer * _rotationSpeed) * _rotationAngle);

            _backBubbles1.Translate(Vector3.up * _back1Speed * Time.deltaTime);
            _backBubbles2.Translate(Vector3.up * _back1Speed * Time.deltaTime);

            if (_backBubbles1.transform.localPosition.y > 1500)
                _backBubbles1.transform.localPosition = -1559.884f * Vector3.up;
            if (_backBubbles2.transform.localPosition.y > 1500)
                _backBubbles2.transform.localPosition = -1559.884f * Vector3.up;

            _middleBubbles1.Translate(Vector3.up * _back2Speed * Time.deltaTime);
            _middleBubbles2.Translate(Vector3.up * _back2Speed * Time.deltaTime);

            if (_middleBubbles1.transform.localPosition.y > 1500)
                _middleBubbles1.transform.localPosition = -1559.884f * Vector3.up;
            if (_middleBubbles2.transform.localPosition.y > 1500)
                _middleBubbles2.transform.localPosition = -1559.884f * Vector3.up;

            _forwardBack.transform.Translate(Vector3.up * Mathf.Sin(_timer * _back3Speed) * _backMoveRange);
        }
    }
}
