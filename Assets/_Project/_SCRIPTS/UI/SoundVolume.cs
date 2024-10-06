using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class SoundVolume : MonoBehaviour
    {
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Image _soundButtonImage;
        [SerializeField] private Sprite _soundOffSprite;
        [SerializeField] private Sprite _soundOnSprite;
         
        private void OnEnable()
        {
            _soundSlider.value = AudioListener.volume;
            _soundSlider.onValueChanged.AddListener(OnVolumeChanged);
            _soundButton.onClick.AddListener(OnClickButton);
        }
   
        private void OnDisable()
        {
            _soundSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            _soundButton.onClick.RemoveListener(OnClickButton);
        }

        private void OnClickButton()
        {
            if (AudioListener.volume > 0)
            {
                _soundButtonImage.sprite = _soundOffSprite;
                AudioListener.volume = 0;
                _soundSlider.value = 0;
            }
            else
            {
                _soundButtonImage.sprite = _soundOnSprite;
                AudioListener.volume = 0.5f;
                _soundSlider.value = 0.5f;
            }
        }

        private void OnVolumeChanged(float volume)
        {
            AudioListener.volume = volume;
            if (volume <= 0)
                _soundButtonImage.sprite = _soundOffSprite;
            else
                _soundButtonImage.sprite = _soundOnSprite;
        }

    }
}
