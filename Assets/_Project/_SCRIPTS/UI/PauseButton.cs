using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _pauseMenu;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Pauser.Instance.Pause();
            _pauseMenu.SetActive(true);
        }
    }
}
