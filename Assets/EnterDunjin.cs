using UnityEngine;
using System.Collections;

public class EnterDunjin : MonoBehaviour
{
    public GameObject[] Dunjins;
    public GameObject ActiveDunjin;
    public int DunjinNumber;
    private GameObject player;
    private Camera mainCamera;

    void Start()
    {   
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        ActiveDunjin = Instantiate(Dunjins[DunjinNumber], transform.position + new Vector3(-2000, 0, 0), Dunjins[DunjinNumber].transform.rotation, transform);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShrinkAndTeleport());
        }
    }

    IEnumerator ShrinkAndTeleport()
    {
        Transform entrance = FindEntranceInGrandchildrenObjects(transform);
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

    Transform FindEntranceInGrandchildrenObjects(Transform parent)
    {
        foreach (Transform child in parent)
        {
            foreach (Transform grandchild in child)
            {
                if (grandchild.CompareTag("Entrance"))
                {
                    return grandchild;
                }
            }
        }
        return null;
    }
}
