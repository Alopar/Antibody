using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Restart);
        }

        private void Restart()
        {
#if UNITY_EDITOR
            EditorSceneManager.LoadSceneInPlayMode(SceneManager.GetActiveScene().path, new LoadSceneParameters());
            Pauser.Instance.Resume();
#else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif
        }
    }
}
