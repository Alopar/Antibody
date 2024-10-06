using Spine;
using Spine.Unity;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected float _health;
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected float _attackDistance;
        [SerializeField] protected float _contactOffset;
        [SerializeField] protected float _attackCooldown;
        [SerializeField] protected int _attackDamage;
        [SerializeField] protected float _changeMarkTime;
        [SerializeField] protected MarkType _markType;
        [SerializeField] protected SkeletonAnimation _skeleton;
        [SerializeField] protected string _skinPrefix;

        protected Cell _cell;
        protected Player _player;
        protected float _timer;
        protected bool _isMarked;

        public MarkType MarkType => _markType;
        public bool IsChangingMark => _markType == MarkType.None;
        public bool IsMarked => _isMarked;

        public event Action Died;
        public event Action Attacked;
        public event Action<Vector3> Moved;

        protected virtual void Awake()
        {
            RandomiseMark();
        }

        public void Init(Player player, Cell cell)
        {
            _player = player;
            _cell = cell;
        }

        protected abstract void Update();
        
        protected abstract void Attack();

        protected abstract void Move();

        public void Mark(MarkType mark)
        {
            if (IsChangingMark || _isMarked)
                return;

            if (mark != _markType)
            {
                WrongMark();
                return;
            }

            _skeleton.Skeleton.SetSkin(_skinPrefix + _markType.ToString() + " active");
            _skeleton.Skeleton.SetSlotsToSetupPose();

            _isMarked = true;
            EnemyManager.Instance.TriggerEnemyMarked(this);
        }

        protected void WrongMark()
        {
            StartCoroutine(ChangingMark());
            _skeleton.Skeleton.SetSkin(_skinPrefix + " question");
            _skeleton.Skeleton.SetSlotsToSetupPose();
        }

        protected void RandomiseMark()
        {
            _markType = (MarkType)UnityEngine.Random.Range(0, WavesManager.Instance.CurrentWave.AllowedMarks);
            _skeleton.Skeleton.SetSkin(_skinPrefix + _markType.ToString() + " passive");
            _skeleton.Skeleton.SetSlotsToSetupPose();
        }

        protected abstract IEnumerator ChangingMark();
        public virtual void Die()
        {
            Destroy(this);
            Destroy(gameObject, 0.7f);
            Died?.Invoke();
        }

        protected void TriggerAttack()
        {
            Attacked?.Invoke();
        }
        
        protected void TriggerMoved(Vector3 direction)
        {
            Moved?.Invoke(direction);
        }
    }
}
