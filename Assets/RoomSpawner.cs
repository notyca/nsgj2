using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 -> Spawn bottom door
    //2 -> Spawn top door
    //3 -> Spawn left door
    //4 -> Spawn right door

    private Vector2[] openingDirectionVectors =
    {
        Vector2.up,
        Vector2.down,
        Vector2.right,
        Vector2.left,
    };

    private int[] openingDirectionBits =
    {
        1,// 0001
        2,// 0010
        4,// 0100
        8,// 1000
    };

    private RoomTemplates templates;
    private int rand;
    private int rand2;
    public bool spawned = false;
    public float waitTime = 4f;
    private Transform Vedal;
    

    void Awake() {
        Vedal = GameObject.Find("RoomTemplates").transform;
        Destroy(gameObject, waitTime);
        GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("Rooms");
        templates = roomObjects[0].GetComponent<RoomTemplates>();
    }

    private void Start()
    {
        Invoke("Spawn", 0.1f);
    }

    void Spawn() {

        if (!templates.roomPositions.Keys.Contains(GetComponentInParent<AddRoom>().roomPosition + openingDirectionVectors[openingDirection - 1])) {
            switch (openingDirection)
            {
                case 1:
                    rand = Random.Range(0, templates.bottom.Length);
                    Transform firstObjectTransform1 = Instantiate(templates.bottom[rand], transform.position, templates.bottom[rand].transform.rotation, Vedal.transform).transform;
                    rand2 = Random.Range(0, templates.inner.Length);
                    Transform secondObjectTransform1 = Instantiate(templates.inner[rand2], transform.position, templates.inner[rand2].transform.rotation, firstObjectTransform1).transform;
                    secondObjectTransform1.localScale /= 1f;

                    firstObjectTransform1.GetComponent<AddRoom>().roomPosition = GetComponentInParent<AddRoom>().roomPosition + Vector2.up;
                    templates.roomPositions[firstObjectTransform1.GetComponent<AddRoom>().roomPosition] = firstObjectTransform1.GetComponent<AddRoom>();
                    break;

                case 2:
                    rand = Random.Range(0, templates.top.Length);
                    Transform firstObjectTransform2 = Instantiate(templates.top[rand], transform.position, templates.top[rand].transform.rotation, Vedal.transform).transform;
                    rand2 = Random.Range(0, templates.inner.Length);
                    Transform secondObjectTransform2 = Instantiate(templates.inner[rand2], transform.position, templates.inner[rand2].transform.rotation, firstObjectTransform2).transform;
                    secondObjectTransform2.localScale /= 1f;

                    firstObjectTransform2.GetComponent<AddRoom>().roomPosition = GetComponentInParent<AddRoom>().roomPosition + Vector2.down;
                    templates.roomPositions[firstObjectTransform2.GetComponent<AddRoom>().roomPosition] = firstObjectTransform2.GetComponent<AddRoom>();
                    break;

                case 3:
                    rand = Random.Range(0, templates.left.Length);
                    Transform firstObjectTransform3 = Instantiate(templates.left[rand], transform.position, templates.left[rand].transform.rotation, Vedal.transform).transform;
                    rand2 = Random.Range(0, templates.inner.Length);
                    Transform secondObjectTransform3 = Instantiate(templates.inner[rand2], transform.position, templates.inner[rand2].transform.rotation, firstObjectTransform3).transform;
                    secondObjectTransform3.localScale /= 1f;

                    firstObjectTransform3.GetComponent<AddRoom>().roomPosition = GetComponentInParent<AddRoom>().roomPosition + Vector2.right;
                    templates.roomPositions[firstObjectTransform3.GetComponent<AddRoom>().roomPosition] = firstObjectTransform3.GetComponent<AddRoom>();
                    break;

                case 4:
                    rand = Random.Range(0, templates.right.Length);
                    Transform firstObjectTransform4 = Instantiate(templates.right[rand], transform.position, templates.right[rand].transform.rotation, Vedal.transform).transform;
                    rand2 = Random.Range(0, templates.inner.Length);
                    Transform secondObjectTransform4 = Instantiate(templates.inner[rand2], transform.position, templates.inner[rand2].transform.rotation, firstObjectTransform4).transform;
                    secondObjectTransform4.localScale /= 1f;

                    firstObjectTransform4.GetComponent<AddRoom>().roomPosition = GetComponentInParent<AddRoom>().roomPosition + Vector2.left;
                    templates.roomPositions[firstObjectTransform4.GetComponent<AddRoom>().roomPosition] = firstObjectTransform4.GetComponent<AddRoom>();
                    break;
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Spawnpoint")) {
            if(GetComponentInParent<AddRoom>() == null)
            {
                print("add room is null");
            }
            if(templates == null)
            {
                print("templates is null");
            }
            if(templates.roomPositions == null)
            {
                print("room positions is null");
            }
            if (templates.roomPositions.Keys == null)
            {
                print("room positions keys is null");
            }
            if (templates.roomPositions.Keys.Contains(GetComponentInParent<AddRoom>().roomPosition + openingDirectionVectors[openingDirection - 1]))
            {
                AddRoom adjacentRoom = templates.roomPositions[GetComponentInParent<AddRoom>().roomPosition + openingDirectionVectors[openingDirection - 1]];
                if(adjacentRoom == null)
                {
                    print("adjacent room is null");
                }
                if ((adjacentRoom.roomType & openingDirectionBits[openingDirection - 1]) == 0)
                {
                    templates.rooms.Remove(adjacentRoom.gameObject);
                    Destroy(adjacentRoom.gameObject);
                    int newRoomIndex = adjacentRoom.roomType | openingDirectionBits[openingDirection - 1];
                    Transform firstObjectTransform1 = Instantiate(templates.roomTypes[newRoomIndex], transform.position, templates.roomTypes[newRoomIndex].transform.rotation, Vedal.transform).transform;
                    Transform secondObjectTransform1 = Instantiate(templates.inner[0], transform.position, templates.inner[0].transform.rotation, firstObjectTransform1).transform;

                    firstObjectTransform1.GetComponent<AddRoom>().roomPosition = GetComponentInParent<AddRoom>().roomPosition + openingDirectionVectors[openingDirection - 1];
                    templates.roomPositions[firstObjectTransform1.GetComponent<AddRoom>().roomPosition] = firstObjectTransform1.GetComponent<AddRoom>();
                }
                
            }
            else
            {
                //spawn wall to block hole
                /*
                Instantiate(templates.block, transform.position, templates.block.transform.rotation, Vedal);
                Destroy(gameObject);
                */
                int newRoomIndex = openingDirectionBits[openingDirection - 1];
                Transform firstObjectTransform1 = Instantiate(templates.roomTypes[newRoomIndex], transform.position, templates.roomTypes[newRoomIndex].transform.rotation, Vedal.transform).transform;
                Transform secondObjectTransform1 = Instantiate(templates.inner[0], transform.position, templates.inner[0].transform.rotation, firstObjectTransform1).transform;

                firstObjectTransform1.GetComponent<AddRoom>().roomPosition = GetComponentInParent<AddRoom>().roomPosition + openingDirectionVectors[openingDirection - 1];
                templates.roomPositions[firstObjectTransform1.GetComponent<AddRoom>().roomPosition] = firstObjectTransform1.GetComponent<AddRoom>();
            }
            spawned = true;
        }
    }
}
