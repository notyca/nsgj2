using UnityEngine;

public class BouncyBullet : MonoBehaviour
{
    [SerializeField] private GameObject destroyedBullet;
    public float speed;

    private PlayerMovement player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player && player.IsInBulletTime())
        {
            GetComponent<Rigidbody2D>().linearVelocity = GetComponent<Rigidbody2D>().linearVelocity.normalized * speed * player.GetBulletTimeSlowDown();
        }
        else
        {
            GetComponent<Rigidbody2D>().linearVelocity = GetComponent<Rigidbody2D>().linearVelocity.normalized * speed;
        }
    }

    int numCollisions = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ++numCollisions;
        if (numCollisions < 2)
            return;
        GameObject newDestroyedBullet = Instantiate(destroyedBullet);
        newDestroyedBullet.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        GameObject newDestroyedBullet = Instantiate(destroyedBullet);
        newDestroyedBullet.transform.position = transform.position;
        Destroy(gameObject);
    }
}
