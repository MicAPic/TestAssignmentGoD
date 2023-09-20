using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int enemyAmount = 3;
    [SerializeField]
    private GameObject[] enemiesTypesToSpawn;
    [SerializeField]
    private Vector2 spawnAreaSize;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < enemyAmount; i++)
        {
            var spawnPos =  new Vector2(
                Random.Range(-spawnAreaSize.x, spawnAreaSize.x), 
                Random.Range(-spawnAreaSize.y, spawnAreaSize.y));
            var enemy = enemiesTypesToSpawn[Random.Range(0, enemiesTypesToSpawn.Length)];

            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}
