using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class IgnorerEnemy : Enemy
    {
        protected override void Update()
        {
            _timer += Time.deltaTime;

            Move();

            if (Vector3.Distance(_cell.transform.position, transform.position) <= _contactOffset)
                Attack();
        }

        protected override void Attack()
        {
            if (_timer < _attackCooldown)
                return;

            _timer = 0;
            _cell.GetComponent<Health>().DealDamage(_attackDamage);
        }

        protected override void Move()
        {
            if (Vector3.Distance(_cell.transform.position, transform.position) <= _contactOffset)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _cell.transform.position, _moveSpeed * Time.deltaTime);
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
