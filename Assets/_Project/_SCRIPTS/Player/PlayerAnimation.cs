using System;
using Spine.Unity;
using UnityEngine;

namespace Gameplay
{
    public class PlayerAnimation : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private SkeletonAnimation _skeleton;
        [SerializeField] private Movement _movement;
        [SerializeField] private Shooting _shooting;
        #endregion

        #region FIELDS PRIVATE
        private string _currentAnimation;
        #endregion

        #region METHODS PRIVATE
        private void UpdateState()
        {
            var animation = "idle";
            if (_movement.IsMove)
            {
                animation = "walk";
            }

            if (_shooting.IsShoot)
            {
                _skeleton.AnimationState.SetAnimation(1, "attack", false);
            }

            if (_currentAnimation == animation) return;
            _skeleton.AnimationName = animation;
        }
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            UpdateState();
        }
        #endregion
    }
}
