using System.Collections.Generic;
using UnityEngine;

public class Necklace : MonoBehaviour
{
    public bool hasNecklace = false;
    public GameObject NecklaceTemplate;
    public List<GameObject> bullets;
    public void SpawnNecklace() {
        Instantiate(NecklaceTemplate, transform.position, NecklaceTemplate.transform.rotation);
        hasNecklace = false;
    }
}
