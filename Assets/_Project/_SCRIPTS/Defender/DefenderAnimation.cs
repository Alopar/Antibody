using Spine.Unity;
using UnityEngine;

namespace Gameplay
{
    public class DefenderAnimation : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeleton;
        [SerializeField] private Defender _defender;
        [SerializeField] private string _idle;
        [SerializeField] private string _attack;
        [SerializeField] private string _walk;
        [SerializeField] private string _eat;

        private bool _initialized;
        private Spine.AnimationState _animation;
        private bool _moving = false;

        private void Start()
        {
            _animation = _skeleton.AnimationState;
            _defender.Attacked += OnAttack;
            _defender.Moved += OnMove;
            _initialized = true;
        }

        private void OnEnable()
        {
            if (!_initialized)
                return;

            _defender.Attacked += OnAttack;
            _defender.Moved += OnMove;
        }

        private void OnMove(Vector3 direction)
        {
            if (!_moving)
            {
                _animation.SetAnimation(0, _walk, true);
                _moving = true;
            }

            if (direction.x > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        private void OnAttack()
        {
            _animation.SetAnimation(0, _attack, false);
            _animation.AddAnimation(0, _eat, false, 0);
            _animation.AddAnimation(0, _idle, true, 0);
        }

        private void OnDisable()
        {
            _defender.Attacked -= OnAttack;
            _defender.Moved -= OnMove;
        }
    }
}
