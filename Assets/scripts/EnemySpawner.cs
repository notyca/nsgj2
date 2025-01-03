using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemieses;

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
                spawnEnemies();
            }

            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if(other.CompareTag("boss spawner"))
        {
            Destroy(gameObject);
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



    void spawnEnemies()
    {
        rand = Random.Range(0, enemieses.Length);
        GameObject enemies = Instantiate(enemieses[rand], transform.position, enemieses[rand].transform.rotation, transform);
        foreach (GameObject enemy in enemies.GetComponent<enemies>().enemyz)
        {
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
                        boxCollider.enabled = false;
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
