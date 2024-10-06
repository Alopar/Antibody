using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Gameplay
{
    public class CameraControl : MonoBehaviour
    {
        #region SINGLETONE
        private static CameraControl _instance;
        public static CameraControl Instance => _instance;
        #endregion

        [SerializeField] private CinemachineCamera _followCamera;
        [SerializeField] private CinemachineCamera _centerCamera;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            GameFlow.Instance.RoundEnded += OnRoundEnded;
        }

        private void OnRoundEnded()
        {
            ShowCenter(3f);
        }

        public void ShowCenter(float time)
        {
            _followCamera.gameObject.SetActive(false);
            StartCoroutine(ReturnFocus(time));
        }

        private IEnumerator ReturnFocus(float afterTime)
        {
            yield return new WaitForSeconds(afterTime);
            _followCamera.gameObject.SetActive(true);
        }
    }
}
