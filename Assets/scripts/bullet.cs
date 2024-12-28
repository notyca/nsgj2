using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private GameObject destroyedBullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject newDestroyedBullet = Instantiate(destroyedBullet);
        newDestroyedBullet.transform.position = transform.position;
        Destroy(gameObject);
    }
}
