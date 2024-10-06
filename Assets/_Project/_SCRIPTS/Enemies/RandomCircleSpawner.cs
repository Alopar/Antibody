using UnityEngine;

namespace Gameplay
{
    public class RandomCircleSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRadius;
        [SerializeField] private float _spawnMaxAngleShift;
        [SerializeField] private float _spawnMinAngleShift;

        private float _timer;
        private float _spawnCooldown;
        private Vector3 _prevSpawnPosition;

        private void Awake()
        {
            _prevSpawnPosition = UnityEngine.Random.insideUnitCircle.normalized * _spawnRadius; 
        }

        private void Start()
        {
            float spawnTimeSpread = WavesManager.Instance.CurrentWave.SpawnCooldown * 0.85f;
            _spawnCooldown = WavesManager.Instance.CurrentWave.SpawnCooldown + Random.Range(-spawnTimeSpread, spawnTimeSpread);
            WavesManager.Instance.WaveNumberIncreased += OnNextWave;
        }

        private void OnNextWave()
        {
            float spawnTimeSpread = WavesManager.Instance.CurrentWave.SpawnCooldown * 0.85f;
            _spawnCooldown = WavesManager.Instance.CurrentWave.SpawnCooldown + Random.Range(-spawnTimeSpread, spawnTimeSpread);
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
                switch(UnityEngine.Random.Range(0, 4))
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
                    case 2:
                        if (WavesManager.Instance.IgnorersToSpawn > 0)
                        {
                            EnemyManager.Instance.SpawnEnemy<IgnorerEnemy>(spawnPos);
                            spawned = true;
                        }
                        break;
                    case 3:
                        if (WavesManager.Instance.WeakCellsAttackersToSpawn > 0)
                        {
                            EnemyManager.Instance.SpawnEnemy<WeakCellFocuserEnemy>(spawnPos);
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
