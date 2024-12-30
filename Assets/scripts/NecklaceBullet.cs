using UnityEngine;

public class NecklaceBullet : MonoBehaviour
{
    [SerializeField] private GameObject destroyedBullet;
    public float speed = 5.0f;

    private PlayerMovement player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        GetComponent<Rigidbody2D>().linearVelocity = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (player && player.IsInBulletTime())
        {
            GetComponent<Rigidbody2D>().linearVelocity = GetComponent<Rigidbody2D>().linearVelocity.normalized * speed * player.GetBulletTimeSlowDown();
        }
        else
        {
            GetComponent<Rigidbody2D>().linearVelocity = GetComponent<Rigidbody2D>().linearVelocity.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject newDestroyedBullet = Instantiate(destroyedBullet);
        newDestroyedBullet.transform.position = transform.position;
        Destroy(gameObject);
    }
}
