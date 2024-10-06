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

            if (_currentAnimation == animation) return;
            _skeleton.AnimationName = animation;
            // _skeleton.AnimationState.SetAnimation(0, "idle", true);
        }
        
        // private void OnMove(Vector3 direction)
        // {
        //     if (!_moving)
        //     {
        //         _animation.SetAnimation(0, _walk, true);
        //         _moving = true;
        //     }
        //
        //     if (direction.x > 0)
        //         transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        //     else
        //         transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        // }
        //
        // private void OnAttack()
        // {
        //     _animation.SetAnimation(0, _attack, false);
        //     _animation.AddAnimation(0, _eat, false, 0);
        //     _animation.AddAnimation(0, _idle, true, 0);
        // }
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            UpdateState();
        }
        #endregion
    }
}
