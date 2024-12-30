using UnityEngine;

public class SpawnDunjin : MonoBehaviour
{
    public GameObject[] Dunjins;
    private GameObject ActiveDunjin;
    public int DunjinNumber;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
                ActiveDunjin = Instantiate(Dunjins[DunjinNumber], transform.position + new Vector3(-200, 1.5f, 0), Dunjins[DunjinNumber].transform.rotation, transform);
            }
    }
}
