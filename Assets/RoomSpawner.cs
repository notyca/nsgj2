using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 -> Spawn bottom door
    //2 -> Spawn top door
    //3 -> Spawn left door
    //4 -> Spawn right door

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    void Start() {
        GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("Rooms");
        templates = roomObjects[0].GetComponent<RoomTemplates>();
        Invoke("Spawn",0.5f);
    }

    void Spawn() {

        if (!spawned) {
            switch (openingDirection)
            {
                case 1:
                    //Spawn bottom room
                    rand = Random.Range(0,templates.bottom.Length);
                    Instantiate(templates.bottom[rand], transform.position, templates.bottom[rand].transform.rotation);
                    break;

                case 2:
                    //Spawn top room
                    rand = Random.Range(0,templates.top.Length);
                    Instantiate(templates.top[rand], transform.position, templates.top[rand].transform.rotation);
                    break;

                case 3:
                    //Spawn left room
                    rand = Random.Range(0,templates.left.Length);
                    Instantiate(templates.left[rand], transform.position, templates.left[rand].transform.rotation);
                    break;

                case 4:
                    //Spawn right room
                    rand = Random.Range(0,templates.right.Length);
                    Instantiate(templates.right[rand], transform.position, templates.right[rand].transform.rotation);
                    break;
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Spawnpoint")) {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false) {
                //spawn wall to block hole
                Instantiate(templates.block, transform.position, templates.block.transform.rotation);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
