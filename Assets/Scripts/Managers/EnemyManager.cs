using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyPath _enemyPath;
    [SerializeField] private float _spawnInterval = 2f;

    private GameObject _enemyPrefab;
    private float _timer = 0f;
    private void Awake()
    {
        _enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Enemy");
       
        if (_enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not found! Make sure it¡¯s under Resources/Enemies/Enemy");
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            EnemySpawn();
            _timer = 0f;
        }
    }
    private void EnemySpawn()
    {
        if (_enemyPrefab == null || _enemyPath == null) return;

        GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
       
        if (enemyScript != null)
        {
            enemyScript.Init(_enemyPath, 3.0f);
        }
    }
}
