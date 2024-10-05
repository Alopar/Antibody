using System;
using System.Collections;
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
        [SerializeField] protected float _attackDamage;
        [SerializeField] protected float _changeMarkTime;
        [SerializeField] protected MarkType _markType;

        protected CellTemp _cell;
        protected PlayerTemp _player;
        protected float _timer;
        protected bool _isMarked;

        public bool IsChangingMark => _markType == MarkType.None;
        public bool IsMarked => _isMarked;

        protected virtual void Awake()
        {
            RandomiseMark();
        }

        public void Init(PlayerTemp player, CellTemp cell)
        {
            _player = player;
            _cell = cell;
        }

        protected abstract void Update();
        
        protected abstract void Attack();

        protected abstract void Move();

        public void Mark(MarkType mark)
        {
            if (IsChangingMark)
                return;

            if (mark != _markType)
            {
                WrongMark();
                return;
            }

            _isMarked = true;
        }

        protected void WrongMark()
        {
            StartCoroutine(ChangingMark());
        }

        protected void RandomiseMark()
        {
            _markType = (MarkType)UnityEngine.Random.Range(0, 3);
        }

        protected abstract IEnumerator ChangingMark();
    }
}
