using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] bosses;

    private RoomTemplates templates;
    private int rand;
    public List<GameObject> Enemys = new List<GameObject>();
    private bool stuffSpawned = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (stuffSpawned)
            return;
        if (other.CompareTag("Player"))
        {
            spawnBoss();
            FindAnyObjectByType<Sounds>().PlayBossMusic();

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



    void spawnBoss()
    {
        GameObject boss = Instantiate(bosses[0], transform.position, bosses[0].transform.rotation, transform);
        Enemys.Add(boss);

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
}
