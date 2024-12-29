using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float waitTime = 4f;
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Spawnpoint")) {
            Destroy(other.gameObject);
        } else if (other.CompareTag("NoFullAutoInBuildings")) {
            Destroy(other.gameObject);
        }
    }

    void Start() {
        Destroy(gameObject, waitTime);
    }
}
            