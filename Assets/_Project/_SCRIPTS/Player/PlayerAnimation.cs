using System;
using Spine.Unity;
using UnityEngine;

namespace Gameplay
{
    public class PlayerAnimation : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private Player _player;
        [SerializeField] private Shooting _shooting;
        [SerializeField] private Movement _movement;
        [SerializeField] private SkeletonAnimation _skeleton;
        #endregion

        #region FIELDS PRIVATE
        private string _currentAnimation;
        #endregion

        #region METHODS PRIVATE
        private void UpdateState()
        {
            var animation = "idle";
            animation = _movement.IsMove ? "walk" : animation;
            animation = _player.IsDie ? "death" : animation;

            if (_shooting.IsShoot)
            {
                _skeleton.AnimationState.SetAnimation(1, "attack", false);
            }

            if (_currentAnimation == animation) return;

            _skeleton.AnimationName = animation;
            _skeleton.loop = animation == "death" ? false : true;
            _skeleton.AnimationState.TimeScale = animation == "death" ? 0.5f : 1f;
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
