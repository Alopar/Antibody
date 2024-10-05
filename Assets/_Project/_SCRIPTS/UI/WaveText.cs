using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class WaveText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private bool _initialized;

        private void Start()
        {
            if (!_initialized)
            {
                _text.text = "Wave:" + WavesManager.Instance.CurrentWave.ToString();
                WavesManager.Instance.WaveNumberIncreased += OnNewWave;
                _initialized = true;
            }
        }

        private void OnEnable()
        {
            if (WavesManager.Instance == null)
                return;
            _text.text = "Wave:" + WavesManager.Instance.CurrentWave.ToString();
            WavesManager.Instance.WaveNumberIncreased += OnNewWave;
            _initialized = true;
        }

        private void OnDisable()
        {
            WavesManager.Instance.WaveNumberIncreased -= OnNewWave;
        }

        private void OnNewWave()
        {
            _text.text = "Wave:" + WavesManager.Instance.CurrentWave.ToString();
        }
    }
}
