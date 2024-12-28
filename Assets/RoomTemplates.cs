using System;
using System.Collections.Generic;
using UnityEngine;

public struct Room
{
    public enum RoomTypes
    {
        ENEMY,
        SHOP,
        BOSS
    }

    public bool up;
    public bool down;
    public bool left;
    public bool right;

    public RoomTypes roomType;
}

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottom;
    public GameObject[] top;
    public GameObject[] left;
    public GameObject[] right;
    public GameObject[] inner;
    public GameObject[] enemy;

    public GameObject block;
    public List<GameObject> rooms;

    public Dictionary<Vector2, Room> roomPositions;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

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
