using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private float bullet_speed = 5.0f;

    private PlayerMovement player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();

        StartCoroutine(PowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PowerUp()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(ShootBullets());
    }

    IEnumerator ShootBullets()
    {
        Vector2 direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector2.up;

        float interval = 1f;

        float current_interval = 0;

        while (true)
        {
            for (int i = 0; i < 3; ++i)
            {
                GameObject new_bullet = Instantiate(bullet);
                new_bullet.transform.position = transform.position;
                new_bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;
                new_bullet.GetComponent<bullet>().speed = bullet_speed;
                direction = Quaternion.AngleAxis(120, Vector3.forward) * direction;
            }
            direction = Quaternion.AngleAxis(20, Vector3.forward) * direction;
            while (current_interval < interval)
            {
                if (player && player.IsInBulletTime())
                {
                    current_interval += Time.deltaTime * player.GetBulletTimeSlowDown();
                }
                else
                {
                    current_interval += Time.deltaTime;
                }
                yield return new WaitForEndOfFrame();
            }
            current_interval = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
