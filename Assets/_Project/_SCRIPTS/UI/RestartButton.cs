using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

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
            UIManager.Instance.Blackout.Blackout(true, false, () => 
            {
#if UNITY_EDITOR
                EditorSceneManager.LoadSceneInPlayMode(SceneManager.GetActiveScene().path, new LoadSceneParameters());
#else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif
                Pauser.Instance.Resume();
            });

        }
    }
}
