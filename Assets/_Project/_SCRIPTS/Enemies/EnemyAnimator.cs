using Spine.Unity;
using UnityEngine;

namespace Gameplay
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private SkeletonAnimation _skeleton;
        [SerializeField] private string _attack;
        [SerializeField] private string _move;
        [SerializeField] private string _die;

        private bool _initialized;
        private Spine.AnimationState _animation;

        private void Start()
        {
            _animation = _skeleton.AnimationState;
            _enemy.Attacked += OnAttack;
            _enemy.Moved += OnMove;
            _enemy.Died += OnDie;
            _initialized = true;
        }

        private void OnEnable()
        {
            if (!_initialized)
                return;

            _enemy.Attacked += OnAttack;
            _enemy.Died += OnDie;
            _enemy.Moved += OnMove;
        }

        private void OnDisable()
        {
            _enemy.Attacked -= OnAttack;
            _enemy.Died -= OnDie;
            _enemy.Moved -= OnMove;
        }

        private void OnAttack()
        {
            _animation.SetAnimation(0, _attack, false);
            _animation.AddAnimation(0, _move, true, 0);
        }

        private void OnDie()
        {
            _animation.SetAnimation(0, _die, false);
        }

        private void OnMove(Vector3 direction)
        {
            if (direction.x < 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
