using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float waitTime = 4f;
    
    void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
    }

    void Start() {
        Destroy(gameObject, waitTime);
    }
}
