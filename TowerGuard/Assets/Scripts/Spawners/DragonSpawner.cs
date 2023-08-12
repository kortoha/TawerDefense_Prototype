using UnityEngine;
using System.Collections;

public class DragonSpawner : MonoBehaviour
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
                DragonMovement dragonMovement = newEnemy.GetComponent<DragonMovement>();
                if (dragonMovement != null && _movementWay != null)
                {
                    dragonMovement.SetMovementArea(targetPosition);
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
