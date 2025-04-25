using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float SpawnInterval = 5f;
    public float SpawnRange = 10f;

    private float _spawnTimer = 0f;


    private void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (_spawnTimer < SpawnInterval)
        {
            _spawnTimer += Time.deltaTime;
            return;
        }

        GameObject enemy = ObjectPool.Instance.GetObject(EPoolType.Enemy);
        if (enemy == null) return;

        Vector3 spawnPosition = new Vector3(
            UnityEngine.Random.value,
            0f,
            UnityEngine.Random.value
        );

        spawnPosition.Normalize();
        spawnPosition *= SpawnRange;
        spawnPosition.y = 1f;

        enemy.transform.position = spawnPosition;

        _spawnTimer = 0f;
    }
}
