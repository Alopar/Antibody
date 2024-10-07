using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private BlackoutControl _blackout;

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
            _blackout.Blackout(true, false, () => SceneManager.LoadScene(1));
        }
    }
}
