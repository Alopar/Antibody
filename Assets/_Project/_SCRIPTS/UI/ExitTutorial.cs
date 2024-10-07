using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class ExitTutorial : MonoBehaviour
    {
        [SerializeField] private BlackoutControl _blackout;

        private bool _canSkip;

        private void Awake()
        {
            StartCoroutine(BlockStart());
            GetComponent<Button>().onClick.AddListener(CancelTutorial);
        }

        private void CancelTutorial()
        {
            if (_canSkip)
            {
                _blackout.Blackout(true, false, () => SceneManager.LoadScene(2));
            }
        }

        private IEnumerator BlockStart()
        {
            yield return new WaitForSeconds(1);
            _canSkip = true;
        }
    }
}
