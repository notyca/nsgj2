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
    private int rand2;
    public bool spawned = false;
    public float waitTime = 4f;
    private Transform Vedal;
    

    void Start() {
        Vedal = GameObject.Find("RoomTemplates").transform;
        Destroy(gameObject, waitTime);
        GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("Rooms");
        templates = roomObjects[0].GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    void Spawn() {

        if (!spawned) {
            switch (openingDirection)
            {
                case 1:
                    rand = Random.Range(0, templates.bottom.Length);
                    Transform firstObjectTransform1 = Instantiate(templates.bottom[rand], transform.position, templates.bottom[rand].transform.rotation, Vedal.transform).transform;
                    rand2 = Random.Range(0, templates.inner.Length);
                    Transform secondObjectTransform1 = Instantiate(templates.inner[rand2], transform.position, templates.inner[rand2].transform.rotation, firstObjectTransform1).transform;
                    secondObjectTransform1.localScale /= 1f;
                    break;

                case 2:
                    rand = Random.Range(0, templates.top.Length);
                    Transform firstObjectTransform2 = Instantiate(templates.top[rand], transform.position, templates.top[rand].transform.rotation, Vedal.transform).transform;
                    rand2 = Random.Range(0, templates.inner.Length);
                    Transform secondObjectTransform2 = Instantiate(templates.inner[rand2], transform.position, templates.inner[rand2].transform.rotation, firstObjectTransform2).transform;
                    secondObjectTransform2.localScale /= 1f;
                    break;

                case 3:
                    rand = Random.Range(0, templates.left.Length);
                    Transform firstObjectTransform3 = Instantiate(templates.left[rand], transform.position, templates.left[rand].transform.rotation, Vedal.transform).transform;
                    rand2 = Random.Range(0, templates.inner.Length);
                    Transform secondObjectTransform3 = Instantiate(templates.inner[rand2], transform.position, templates.inner[rand2].transform.rotation, firstObjectTransform3).transform;
                    secondObjectTransform3.localScale /= 1f;
                    break;

                case 4:
                    rand = Random.Range(0, templates.right.Length);
                    Transform firstObjectTransform4 = Instantiate(templates.right[rand], transform.position, templates.right[rand].transform.rotation, Vedal.transform).transform;
                    rand2 = Random.Range(0, templates.inner.Length);
                    Transform secondObjectTransform4 = Instantiate(templates.inner[rand2], transform.position, templates.inner[rand2].transform.rotation, firstObjectTransform4).transform;
                    secondObjectTransform4.localScale /= 1f;
                    break;
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Spawnpoint")) {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false) {
                //spawn wall to block hole
                Instantiate(templates.block, transform.position, templates.block.transform.rotation, Vedal);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
