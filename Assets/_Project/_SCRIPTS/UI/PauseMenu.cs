using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _unpauseButton;

        private void OnEnable()
        {
            _unpauseButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _unpauseButton.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Pauser.Instance.Resume();
            gameObject.SetActive(false);
        }
    }
}
