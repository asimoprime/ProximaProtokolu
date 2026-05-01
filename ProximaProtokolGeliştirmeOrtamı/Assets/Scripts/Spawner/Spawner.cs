using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform[] spawnPoints1;
    [SerializeField] Transform[] spawnPoints2;
    bool isSpawned = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSpawned)
        {
            isSpawned = true;
            SpawnEnemy();
        }
    }
    
    private void SpawnEnemy()
    {
        for(int i = 0; i < spawnPoints1.Length; i++)
        {
            Instantiate(enemyPrefabs[0], spawnPoints1[i].position, Quaternion.identity);
        }
        for(int i = 0; i < spawnPoints2.Length; i++)
        {
            Instantiate(enemyPrefabs[1], spawnPoints2[i].position, Quaternion.identity);
        }
    }
}
