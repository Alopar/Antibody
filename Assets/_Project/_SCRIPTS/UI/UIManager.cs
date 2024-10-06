using UnityEngine;

namespace Gameplay
{
    public class UIManager : MonoBehaviour
    {
        #region SINGLETONE
        private static UIManager _instance;
        public static UIManager Instance => _instance;
        #endregion

        [SerializeField] private GameObject _defeatMenu;

        public GameObject DefeatMenu => _defeatMenu;

        private void Awake()
        {
            _instance = this;
        }
    }
}
