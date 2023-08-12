using UnityEngine;
using System.Collections;

public class SpiderSpawner : MonoBehaviour
{
    
    [SerializeField] private Transform _enemyPrefab;
    [SerializeField] private Collider[] _movementArea;

    private bool _isSpawning = true;

    private Coroutine _startCoroutine;

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (_isSpawning)
        {
            float timeToSpawn = 1f;
            yield return new WaitForSeconds(timeToSpawn);

            if (MainTower.Instance != null)
            {
                Transform newEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                SpiderMovement spiderMovement = newEnemy.GetComponent<SpiderMovement>();
                if (spiderMovement != null && _movementArea.Length > 0)
                {
                    int randomIndex = Random.Range(0, _movementArea.Length);
                    spiderMovement.SetMovementArea(_movementArea[randomIndex]);
                }
            }
            else
            {
                _isSpawning = false;
            }
        }
    }

    private void OnEnable()
    {
        _startCoroutine = StartCoroutine(SpawnEnemiesCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_startCoroutine);
    }
}

