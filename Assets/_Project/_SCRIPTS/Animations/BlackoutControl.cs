using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class BlackoutControl : MonoBehaviour
    {
        [SerializeField] private Image _blackoutImage;
        [SerializeField] private float _blackoutSpeed;
        [SerializeField] private bool _enableOnStart;

        private void Awake()
        {
            if (_enableOnStart)
                Blackout(false, true, null);
        }

        private IEnumerator FadeOut(System.Action callback, bool disableImageInEnd)
        {
            Color blackout = new Color(
                _blackoutImage.color.r,
                _blackoutImage.color.g,
                _blackoutImage.color.b,
                1);
            _blackoutImage.color = blackout;

            while (blackout.a > 0)
            {
                yield return 0;
                blackout.a -= _blackoutSpeed * Time.unscaledDeltaTime;
                _blackoutImage.color = blackout;
            }

            yield return new WaitForSecondsRealtime(0.3f);
            callback?.Invoke();

            if (disableImageInEnd)
                _blackoutImage.enabled = false;
        }

        private IEnumerator FadeIn(System.Action callback, bool disableImageInEnd)
        {
            Color blackout = new Color(
                _blackoutImage.color.r,
                _blackoutImage.color.g,
                _blackoutImage.color.b,
                0);
            _blackoutImage.color = blackout;

            while (blackout.a < 1)
            {
                blackout.a += _blackoutSpeed * Time.unscaledDeltaTime;
                _blackoutImage.color = blackout;
                yield return 0;
            }

            yield return new WaitForSecondsRealtime(0.3f);
            callback?.Invoke();

            if (disableImageInEnd)
                _blackoutImage.enabled = false;
        }

        public void Blackout(bool fadeIn, bool disableImageInEnd, System.Action callback)
        {
            _blackoutImage.enabled = true;

            if (fadeIn)
                StartCoroutine(FadeIn(callback, disableImageInEnd));
            else
                StartCoroutine(FadeOut(callback, disableImageInEnd));
        }
    }
}
