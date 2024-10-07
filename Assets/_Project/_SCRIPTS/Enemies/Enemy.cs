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
        [SerializeField] protected float _speedSpread;
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

        public virtual void Init(Player player)
        {
            _player = player;
            ChooseCell();
            Cell.CellDied += OnCellDie;
            _moveSpeed += UnityEngine.Random.Range(-1, 1f) * _speedSpread;
        }

        protected void OnCellDie(Cell cell)
        {
            if (_cell == cell)
                ChooseCell();
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
            SoundManager.Instance.PlayShort(ShortClip.hit, 0);
            EnemyManager.Instance.TriggerEnemyMarked(this);
        }

        protected void WrongMark()
        {
            StartCoroutine(ChangingMark());
            _skeleton.Skeleton.SetSkin(_skinPrefix + " question");
            _skeleton.Skeleton.SetSlotsToSetupPose();
            SoundManager.Instance.PlayShort(ShortClip.wrongShot, 0);
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

        protected virtual void ChooseCell()
        {
            if (Cell.CellsCount == 0)
                return;

            var cells = FindObjectsByType<Cell>(FindObjectsSortMode.None);
            for (int i = 0; i < 100; i++)
            {
                var cell = cells[UnityEngine.Random.Range(0, cells.Length)];
                if (cell != _cell)
                {
                    _cell = cell;
                    return;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            Cell.CellDied -= OnCellDie;
        }
    }
}
