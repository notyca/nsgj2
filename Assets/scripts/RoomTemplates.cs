using System;
using System.Collections;
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
    private GameObject CurrentBoss;

    private GameObject player;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        roomPositions[Vector2.zero] = spawn;
    }

    void Update() {
        if (waitTime <= 0 && spawnedBoss == false) {
            for (int i = 0; i < rooms.Count; i++) {
                if (i == rooms.Count-1) {
                    CurrentBoss = Instantiate(boss, rooms[i].transform.position, Quaternion.identity, rooms[i].transform);
                    spawnedBoss = true;
                }
            }
        } else {
            waitTime -= Time.deltaTime;
        }

        if (spawnedBoss && CurrentBoss == null) {

            StartCoroutine(ShrinkAndTeleport());
            spawnedBoss = false;
            Destroy(gameObject,3f); //Suicide
        }
    }

    IEnumerator ShrinkAndTeleport()
    {
        Transform entrance = GameObject.FindGameObjectWithTag("ReturnPoint").transform;
        if (entrance != null)
        {
            //disable movement
            Vector3 originalScale = player.transform.localScale;
            Vector3 targetScale = new Vector3(0.1f, 0.1f, 1);
            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                player.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            player.transform.localScale = targetScale;

            player.transform.position = entrance.position;
            mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, mainCamera.transform.position.z);

            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;
            while (elapsedTime < 1.5f)
            {
                player.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / 1.5f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            player.transform.localScale = originalScale;
            //reenable movement
        }
    }

}
