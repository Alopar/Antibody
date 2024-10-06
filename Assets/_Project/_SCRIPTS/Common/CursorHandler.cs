using System;
using UnityEngine;

namespace Gameplay
{
    public class CursorHandler : MonoBehaviour
    {
        #region UNITY CALLBACKS
        private void OnMouseEnter()
        {
            CursorSwitcher.Instance.SetAttackState();
        }

        private void OnMouseExit()
        {
            CursorSwitcher.Instance.SetNormalState();
        }
        #endregion
    }
}
