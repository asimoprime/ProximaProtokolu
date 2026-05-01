using UnityEngine;

public class SpawnerWave : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform[] spawnPoints1;
    [SerializeField] Transform[] spawnPoints2;
    [SerializeField] Transform[] spawnPoints3;
    [SerializeField] Transform[] spawnPoints4;
    [SerializeField] int waveCount = 1;
    [SerializeField] float spawnTime = 1f;
    [SerializeField]int enemy1Spawn;
    [SerializeField]int enemy2Spawn;
    [SerializeField]int enemy3Spawn;
    [SerializeField]int enemy4Spawn;
    bool isSpawned = false;
    bool isWaveActive = false;
    float timer = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSpawned)
        {
            isWaveActive = true;
            SpawnEnemy();
        }
    }
    private void Update()
    {
        if(isSpawned||!isWaveActive)
        {
            return;
        }
        timer += Time.deltaTime;
        if(timer > spawnTime)
        {
            SpawnEnemy();
            timer = 0;
        }
    }
    private void SpawnEnemy()
    {   
        for (int i = 0; i < enemy1Spawn; i++)
        {
            Instantiate(enemyPrefabs[0], spawnPoints1[Random.Range(0, spawnPoints1.Length)].position, Quaternion.identity);
        }
        for (int i = 0; i < enemy2Spawn; i++)
        {
            Instantiate(enemyPrefabs[1], spawnPoints2[Random.Range(0, spawnPoints2.Length)].position, Quaternion.identity);
        }
        for (int i = 0; i < enemy3Spawn; i++)
        {
            Instantiate(enemyPrefabs[2], spawnPoints3[Random.Range(0, spawnPoints3.Length)].position, Quaternion.identity);
        }
        if(enemyPrefabs[3]!=null)
        {
            for (int i = 0; i < enemy4Spawn; i++)
            {
                Instantiate(enemyPrefabs[3], spawnPoints4[Random.Range(0, spawnPoints4.Length)].position, Quaternion.identity);
            }
        }
        waveCount--;
        if(waveCount == 0)
        {
            isWaveActive = false;
            isSpawned = true;
        }
    }
}
