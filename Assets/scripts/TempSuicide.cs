using UnityEngine;

public class TempSuicide : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Destroy(gameObject); //Suicide
        }
    }
}
