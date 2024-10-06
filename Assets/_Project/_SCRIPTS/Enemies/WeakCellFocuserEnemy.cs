using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class WeakCellFocuserEnemy : Enemy
    {
        private void Start()
        {
            EnemyManager.Instance.EnemyMarked += OnMarked;
        }

        private void OnMarked(Enemy enemy)
        {
            if (enemy != this)
                return;

            ChooseWeakestCell();
        }

        protected override void Update()
        {
            _timer += Time.deltaTime;

            Move();

            if (_isMarked)
            {
                if (Vector3.Distance(_cell.transform.position, transform.position) <= _contactOffset)
                    Attack();
            }
            else if (Vector3.Distance(_player.transform.position, transform.position) <= _contactOffset)
                Attack();
        }

        protected override void Attack()
        {
            if (_timer < _attackCooldown)
                return;

            _timer = 0;

            if (_isMarked)
                _cell.GetComponent<Health>().DealDamage(_attackDamage);
            else
                _player.GetComponent<Health>().DealDamage(_attackDamage);

            TriggerAttack();
        }

        protected override void Move()
        {
            if (IsMarked)
            {
                MoveMarked();
                return;
            }

            if (Vector3.Distance(_player.transform.position, transform.position) <= _contactOffset)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
            TriggerMoved(_player.transform.position - transform.position);
        }

        private void MoveMarked()
        {
            if (Vector3.Distance(_cell.transform.position, transform.position) <= _contactOffset)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _cell.transform.position, _moveSpeed * Time.deltaTime);
            TriggerMoved(_cell.transform.position - transform.position);
        }

        protected override IEnumerator ChangingMark()
        {
            _markType = MarkType.None;

            // sprites change etc

            yield return new WaitForSeconds(_changeMarkTime);
            RandomiseMark();
        }

        private void ChooseWeakestCell()
        {
            var aliveCells = FindObjectsByType<Cell>(FindObjectsSortMode.None);

            Cell weakestCell = aliveCells[0];
            Health weakest = weakestCell.GetComponent<Health>();
            foreach (var cell in aliveCells)
            {
                var health = cell.GetComponent<Health>();
                if (health.Current < weakest.Current)
                {
                    weakestCell = cell;
                    weakest = health;
                }
            }

            _cell = weakestCell;
        }

        private void OnDestroy()
        {
            EnemyManager.Instance.EnemyMarked -= OnMarked;
        }
    }
}
