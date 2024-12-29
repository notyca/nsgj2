using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private RoomTemplates templates;
    private int EnemyCount;
    private int CoinFlip;
    private int rand;
    public List<GameObject> Enemys = new List<GameObject>();
    private bool stuffSpawned = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("Rooms");
            templates = roomObjects[0].GetComponent<RoomTemplates>();

            EnemyCount = Random.Range(0, 4);
            if (EnemyCount == 0)
            {
                CoinFlip = Random.Range(0, 3);
            }

            if (CoinFlip != 0)
            {
                spawnChest();
            }
            else
            {
                EnemyCount = Random.Range(1, 4);
            }

            spawnEnemies(EnemyCount);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void Update()
    {
        for (int i = Enemys.Count - 1; i >= 0; i--)
        {
            if (Enemys[i] == null)
            {
                Enemys.RemoveAt(i);
            }
        }

        if (Enemys.Count == 0 && stuffSpawned)
        {
            Transform parentTransform = transform.parent;
            
            //enable doors
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                Transform sibling = parentTransform.GetChild(i);

                if (sibling.name == "Doors")
                {
                    foreach (Transform child in sibling)
                    {
                        BoxCollider2D boxCollider = child.GetComponent<BoxCollider2D>();
                        if (boxCollider != null)
                        {
                            boxCollider.enabled = true;
                        }
                    }
                    break;
                }
            }

            Destroy(gameObject); //Suicide
        }
    }



    void spawnEnemies(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            Vector3 spawnSpot = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-1.5f, 1.5f), 0);
            int rand = Random.Range(0, templates.enemy.Length);
            GameObject enemy = Instantiate(templates.enemy[rand], spawnSpot, templates.enemy[rand].transform.rotation, transform);
            Enemys.Add(enemy);
        }

        Transform parentTransform = transform.parent;

        //disable doors
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform sibling = parentTransform.GetChild(i);

            if (sibling.name == "Doors")
            {
                foreach (Transform child in sibling)
                {
                    BoxCollider2D boxCollider = child.GetComponent<BoxCollider2D>();
                    if (boxCollider != null)
                    {
                        boxCollider.enabled = true;
                        stuffSpawned = true;
                    }
                }
                break;
            }
        }
    }


    void spawnChest()
    {
        GameObject Chest = Instantiate(templates.chest, transform.position + Vector3.down, templates.chest.transform.rotation);
        Enemys.Add(Chest);
    }
}
