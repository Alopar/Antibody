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

            if (WavesManager.Instance.RemainingEnemiesToSpawn == 0)
                return;

            float randAngle = UnityEngine.Random.Range(_spawnMinAngleShift, _spawnMaxAngleShift);
            var spawnPos = Quaternion.AngleAxis(randAngle, Vector3.back) * _prevSpawnPosition;
            
            bool spawned = false;
            while (!spawned)
                switch(UnityEngine.Random.Range(0, 2))
                {
                    case 0:
                        if (WavesManager.Instance.FollowersToSpawn > 0)
                        {
                            EnemyManager.Instance.SpawnEnemy<FollowPlayerEnemy>(spawnPos);
                            spawned = true;
                        }
                        break;
                    case 1:
                        if (WavesManager.Instance.CellFocusersToSpawn > 0)
                        {
                            EnemyManager.Instance.SpawnEnemy<PriorityCellEnemy>(spawnPos);
                            spawned = true;
                        }
                        break;
                }

            _prevSpawnPosition = spawnPos;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Vector3.zero, _spawnRadius);
        }
    }
}
