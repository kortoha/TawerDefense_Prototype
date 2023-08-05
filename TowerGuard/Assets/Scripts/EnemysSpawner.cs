using System.Collections;
using UnityEngine;

public class EnemysSpawner : MonoBehaviour
{
    [SerializeField] private Transform _enemyPrefab;
    [SerializeField] private Collider[] _movementArea;

    private bool _isSpawning = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (_isSpawning)
        {
            float timeToSpawn = Random.Range(3f, 5f);
            yield return new WaitForSeconds(timeToSpawn);

            if (MainTower.Instance != null)
            {
                Transform newEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                GoblinsMovement goblinsMovement = newEnemy.GetComponent<GoblinsMovement>();
                if (goblinsMovement != null && _movementArea.Length > 0)
                {
                    int randomIndex = Random.Range(0, _movementArea.Length);
                    goblinsMovement.SetMovementArea(_movementArea[randomIndex]);
                }
            }
            else
            {
                _isSpawning = false;
            }
        }
    }
}