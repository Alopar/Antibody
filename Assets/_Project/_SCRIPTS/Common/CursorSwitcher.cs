using System;
using UnityEngine;

namespace Gameplay
{
    public class CursorSwitcher : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private Texture2D _normal;
        [SerializeField] private Texture2D _attack;
        #endregion

        #region FIELDS PRIVATE
        private static CursorSwitcher _instance;
        #endregion

        #region PROPERTIES
        public static CursorSwitcher Instance => _instance;
        #endregion

        #region METHODS PUBLIC
        public void SetNormalState()
        {
            Cursor.SetCursor(_normal, Vector2.zero, CursorMode.ForceSoftware);
        }

        public void SetAttackState()
        {
            Cursor.SetCursor(_attack, Vector2.zero, CursorMode.ForceSoftware);
        }
        #endregion

        #region METHODS PRIVATE
        private void Init()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            Cursor.SetCursor(_normal, Vector2.zero, CursorMode.ForceSoftware);
        }
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            Init();
        }
        #endregion
    }
}
