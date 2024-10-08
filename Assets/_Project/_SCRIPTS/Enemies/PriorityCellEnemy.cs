﻿using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class PriorityCellEnemy : Enemy
    {
        protected override void Update()
        {
            _timer += Time.deltaTime;

            Move();

            if (_isMarked)
            {
                if (Vector3.Distance(_player.transform.position, transform.position) <= _contactOffset)
                    Attack();
            }
            else if (Vector3.Distance(_cell.transform.position, transform.position) <= _contactOffset)
                Attack();
        }

        protected override void Attack()
        {
            if (_timer < _attackCooldown)
                return;

            _timer = 0;

            if (_isMarked)
            {
                if (_player.IsDie)
                    return;
                else
                    _player.GetComponent<PlayerHealth>().DealDamage(_attackDamage);
            }
            else
                _cell.GetComponent<Health>().DealDamage(_attackDamage);

            TriggerAttack();
        }

        protected override void Move()
        {
            if (IsMarked)
            {
                MoveMarked();
                return;
            }

            if (Vector3.Distance(_cell.transform.position, transform.position) <= _contactOffset)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _cell.transform.position, _moveSpeed * Time.deltaTime);
            TriggerMoved(_cell.transform.position - transform.position);
        }

        private void MoveMarked()
        {
            if (Vector3.Distance(_player.transform.position, transform.position) <= _contactOffset)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
            TriggerMoved(_player.transform.position - transform.position);
        }

        protected override IEnumerator ChangingMark()
        {
            _markType = MarkType.None;

            // sprites change etc

            yield return new WaitForSeconds(_changeMarkTime);
            RandomiseMark();
        }
    }
}
