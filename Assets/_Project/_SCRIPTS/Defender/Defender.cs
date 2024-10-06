using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Defender : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _eatAnimationTime;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private MarkType _markerType;

        private List<Enemy> _markedEnemies = new();
        private float _timer;

        public event Action Attacked;
        public event Action<Vector3> Moved;

        private void Start()
        {
            EnemyManager.Instance.EnemyMarked += OnEnemyMarked;
        }

        private void OnEnemyMarked(Enemy enemy)
        {
            if (enemy.MarkType == _markerType)
                _markedEnemies.Add(enemy);
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_markedEnemies.Count == 0)
                return;

            Move();
            Attack();
        }

        private void Attack()
        {
            if (_timer < _attackCooldown)
                return;

            if (Vector3.Distance(_markedEnemies[0].transform.position, transform.position) > _attackRadius)
                return;

            EnemyManager.Instance.Kill(_markedEnemies[0]);
            _markedEnemies.RemoveAt(0);
            _timer = 0;

            Attacked?.Invoke();
        }

        private void Move()
        {
            if (_timer >= _eatAnimationTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, _markedEnemies[0].transform.position, _moveSpeed * Time.deltaTime);
                Moved?.Invoke(_markedEnemies[0].transform.position - transform.position);
            }
        }
    }
}