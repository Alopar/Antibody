using System;
using UnityEngine;

namespace Gameplay
{
    public class CoursorSwitcher : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private Texture2D _normal;
        [SerializeField] private Texture2D _attack;
        #endregion

        #region UNITY CALLBACKS
        private void OnMouseEnter()
        {
            Cursor.SetCursor(_attack, Vector2.zero, CursorMode.Auto);
        }

        private void OnMouseExit()
        {
            Cursor.SetCursor(_normal, Vector2.zero, CursorMode.Auto);
        }
        #endregion
    }
}
