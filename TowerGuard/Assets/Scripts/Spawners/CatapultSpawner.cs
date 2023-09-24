using UnityEngine;
using System.Collections;

public class CatapultSpawner : MonoBehaviour
{
    [SerializeField] private Transform _enemyPrefab;
    [SerializeField] private Transform _movementWay;

    private bool _isSpawning = true;

    private Coroutine _startCoroutine;

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (_isSpawning)
        {
            Vector3 targetPosition = _movementWay.position;
            float timeToSpawn = 1f;
            yield return new WaitForSeconds(timeToSpawn);

            if (MainTower.Instance != null)
            {
                Transform newEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                CatapultMovement catapultMovement = newEnemy.GetComponent<CatapultMovement>();
                if (catapultMovement != null && _movementWay != null)
                {
                    catapultMovement.SetMovementArea(targetPosition);
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
