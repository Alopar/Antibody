using UnityEngine;

namespace Gameplay
{
    public class RandomCircleSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRadius;
        [SerializeField] private float _spawnMaxAngleShift;
        [SerializeField] private float _spawnMinAngleShift;

        private float _timer;
        private Vector3 _prevSpawnPosition;

        private void Awake()
        {
            _prevSpawnPosition = UnityEngine.Random.insideUnitCircle.normalized * _spawnRadius; 
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= WavesManager.Instance.CurrentWave.SpawnCooldown)
                Spawn();
        }

        private void Spawn()
        {
            _timer = 0;

            float randAngle = UnityEngine.Random.Range(_spawnMinAngleShift, _spawnMaxAngleShift);
            var spawnPos = Quaternion.AngleAxis(randAngle, Vector3.back) * _prevSpawnPosition;

            if (UnityEngine.Random.Range(0, 2) == 0)
                EnemyManager.Instance.SpawnEnemy<FollowPlayerEnemy>(spawnPos);
            else
                EnemyManager.Instance.SpawnEnemy<PriorityCellEnemy>(spawnPos);

            _prevSpawnPosition = spawnPos;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Vector3.zero, _spawnRadius);
        }
    }
}
