using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private RoomTemplates templates;
    private int EnemyCount;
    private int CoinFlip;
    private int rand;
    
    void Start()
    {
        GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("Rooms");
        templates = roomObjects[0].GetComponent<RoomTemplates>();

        EnemyCount = Random.Range(0, 4);
        if (EnemyCount < 1) {
            CoinFlip = Random.Range(0, 2);
        }

        if (CoinFlip == 1) {
            spawnShop();
        } else {
            EnemyCount = Random.Range(1, 4);
        }

        spawnEnemies(EnemyCount);
        
    }

    void spawnEnemies(int Count)
    {
        for (int i = 0; i < Count; i++) 
        {
            Vector3 spawnSpot = transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
            int rand = Random.Range(0, templates.enemy.Length);
            Instantiate(templates.enemy[rand], spawnSpot, templates.enemy[rand].transform.rotation, transform);
        }
    }


    void spawnShop() {

    }

}
