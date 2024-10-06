using UnityEngine;

namespace Gameplay
{
    public class GameFlow : MonoBehaviour
    {
        #region SINGLETONE
        private static GameFlow _instance;
        public static GameFlow Instance => _instance;
        #endregion

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            EnemyManager.Instance.EnemyKilled += OnEnemyKilled;
        }

        private void OnEnemyKilled(Enemy enemy)
        {
            if (EnemyManager.Instance.EnemiesCount == 0)
            {
                WavesManager.Instance.NextWave();
            }
        }

        public void StartGame()
        {
            UIManager.Instance.StartMenu.SetActive(false);
        }
    }

    public class UIManager : MonoBehaviour
    {
        #region SINGLETONE
        private static UIManager _instance;
        public static UIManager Instance => _instance;
        #endregion

        [SerializeField] private GameObject _startMenu;

        public GameObject StartMenu => _startMenu;

        private void Awake()
        {
            _instance = this;
        }
    }
}
