using System;
using UnityEngine;

namespace Gameplay
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _contactOffset;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _attackDamage;
        [SerializeField] private MarkType _mark;

        private PlayerTemp _player;
        private float _timer;

        private void Awake()
        {
            _player = FindAnyObjectByType<PlayerTemp>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            MoveToPlayer();

            if (Vector3.Distance(_player.transform.position, transform.position) <= _contactOffset)
                Attack();
        }

        private void Attack()
        {
            if (_timer < _attackCooldown)
                return;

            _timer = 0;
            Debug.Log("Attacking Player");
        }

        private void MoveToPlayer()
        {
            if (Vector3.Distance(_player.transform.position, transform.position) <= _contactOffset)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
        }
    }
}
