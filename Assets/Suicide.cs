using UnityEngine;

public class Suicide : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f); // Suicide
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
