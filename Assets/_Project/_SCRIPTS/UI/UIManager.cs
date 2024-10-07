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
        [SerializeField] private BlackoutControl _blackout;

        public GameObject DefeatMenu => _defeatMenu;
        public BlackoutControl Blackout => _blackout;

        private void Awake()
        {
            _instance = this;
        }
    }
}
