using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Gameplay
{
    public class ScaleTween : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _scaleTime;
        [SerializeField] private float _overshoot;
        [SerializeField] private bool _startWithZeroScale;
        [SerializeField] private bool _ignoreTimeScale;
        [SerializeField] private bool _scaleToObjectInitialScale;
        [SerializeField, HideIf("_scaleToObjectInitialScale")] private float _scale;
        [SerializeField] private Ease _ease;

        private Tween _cachedTween;

        private void OnEnable()
        {
            if (_scaleToObjectInitialScale)
                _scale = transform.localScale.x;

            if (_startWithZeroScale)
                transform.localScale = Vector3.zero;

            _cachedTween = transform.DOScale(_scale, _scaleTime).SetEase(_ease, _overshoot).SetDelay(_delay);
            if (_ignoreTimeScale) _cachedTween.SetUpdate(UpdateType.Normal, _ignoreTimeScale);
        }
        
        private void OnDisable()
        {
            if (_cachedTween != null && _cachedTween.active)
            {
                _cachedTween.Kill();
                transform.localScale = Vector3.one * _scale;
            }
        }
    }
}
