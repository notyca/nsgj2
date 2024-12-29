using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Chest : MonoBehaviour
{
    private RoomTemplates templates;
    private int rand;
    private bool StartDoinTheThing = false;
    public GameObject activeChoice;
    public int activeChoiceNum;
    private int SelectedId;
    private GameObject highlight;

    public List<GameObject> Powerups = new List<GameObject>();

    void Update()
    {
        if (StartDoinTheThing) {
            highlight.transform.position = activeChoice.transform.position;

            if (Input.GetKeyDown(KeyCode.A)) {
                activeChoiceNum = Mathf.Max(0, activeChoiceNum - 1);
                activeChoice = Powerups[activeChoiceNum];
            } else if (Input.GetKeyDown(KeyCode.D)) {
                activeChoiceNum = Mathf.Min(Powerups.Count - 1, activeChoiceNum + 1);
                activeChoice = Powerups[activeChoiceNum];
            } else if (Input.GetKeyDown(KeyCode.Space)) {
                if (activeChoiceNum == 3) {
                    GivePowerUp(0);
                } else {
                    SelectedId = GetComponent<PwrId>().PowerupId;
                    GivePowerUp(SelectedId); 
                }
                
            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("Rooms");
            templates = roomObjects[0].GetComponent<RoomTemplates>();

            rand = Random.Range(0, templates.ChestLoot.Length);

            var Loot1 = Instantiate(templates.ChestLoot[rand], transform.position + new Vector3(-2, 1.5f, 0), templates.inner[rand].transform.rotation, transform);
            Loot1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            StartCoroutine(ScaleOverTime(Loot1.transform, new Vector3(templates.ChestLoot[rand].transform.localScale.x /2, templates.ChestLoot[rand].transform.localScale.y, templates.ChestLoot[rand].transform.localScale.z), .5f));
            Powerups.Add(Loot1);

            var Loot2 = Instantiate(templates.ChestLoot[rand], transform.position + new Vector3(0, 2.5f, 0), templates.inner[rand].transform.rotation, transform);
            Loot2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            StartCoroutine(ScaleOverTime(Loot2.transform, new Vector3(templates.ChestLoot[rand].transform.localScale.x /2, templates.ChestLoot[rand].transform.localScale.y, templates.ChestLoot[rand].transform.localScale.z), .5f));
            Powerups.Add(Loot2);

            var Loot3 = Instantiate(templates.ChestLoot[rand], transform.position + new Vector3(2, 1.5f, 0), templates.inner[rand].transform.rotation, transform);
            Loot3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            StartCoroutine(ScaleOverTime(Loot3.transform, new Vector3(templates.ChestLoot[rand].transform.localScale.x /2, templates.ChestLoot[rand].transform.localScale.y, templates.ChestLoot[rand].transform.localScale.z), .5f));
            Powerups.Add(Loot3);



            var skipButton = Instantiate(templates.skip, transform.position + new Vector3(4, 0, 0), templates.skip.transform.rotation, transform);
            Powerups.Add(skipButton);

            StartDoinTheThing = true;

            //disable movement here

            activeChoice = Loot1;
            activeChoiceNum = 0;
            GetComponent<BoxCollider2D>().enabled = false;

            highlight = Instantiate(templates.highlight, transform.position + new Vector3(4000, 0, 0), templates.highlight.transform.rotation, transform);
            highlight.transform.localScale = new Vector3(highlight.transform.localScale.x / 2, highlight.transform.localScale.y, highlight.transform.localScale.z);
        }
    }


    void GivePowerUp(int PowerupId) {
        //reenable movement here

        switch (PowerupId)
            {
                case 0:
                    //skip
                    break;
                case 1:
                    print("Test");
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                    
                    break;
            }

        Destroy(gameObject); //Suicide
    }

    private IEnumerator ScaleOverTime(Transform target, Vector3 targetScale, float duration)
    {
        Vector3 initialScale = target.localScale;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            target.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.localScale = targetScale;
    }


}
