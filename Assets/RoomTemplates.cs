using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottom;
    public GameObject[] top;
    public GameObject[] left;
    public GameObject[] right;
    public GameObject[] inner;
    public GameObject[] enemy;
    public GameObject[] ChestLoot;

    public GameObject block;
    public GameObject chest;
    public GameObject skip;
    public GameObject highlight;
    public List<GameObject> rooms;

    public Dictionary<Vector2, AddRoom> roomPositions = new Dictionary<Vector2, AddRoom>();
    public GameObject[] roomTypes;
    public AddRoom spawn;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    private void Start()
    {
        roomPositions[Vector2.zero] = spawn;
    }

    void Update() {
        if (waitTime <= 0 && spawnedBoss == false) {
            for (int i = 0; i < rooms.Count; i++) {
                if (i == rooms.Count-1) {
                    Instantiate(boss,rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        } else {
            waitTime -= Time.deltaTime;
        }
    }
}
