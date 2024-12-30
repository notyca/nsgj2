using System.Collections;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    [SerializeField] private GameObject puffCloud;
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
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(1f);
        GameObject newPuffCloud = Instantiate(puffCloud);
        newPuffCloud.transform.position = transform.position;
        float elapsed = 0;
        while(elapsed < 2f)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Min(elapsed / 0.2f, 1));
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(newPuffCloud);
        StartCoroutine(ShootAtPlayer());
    }

    IEnumerator ShootAtPlayer()
    {
        Vector2 direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector2.up;

        float interval = 1f;

        float current_interval = 0;

        while(true)
        {
            direction = (FindAnyObjectByType<PlayerMovement>().transform.position - transform.position).normalized;
            GameObject new_bullet = Instantiate(bullet);
            new_bullet.transform.position = transform.position;
            new_bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;
            new_bullet.GetComponent<bullet>().speed = bullet_speed;
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
                yield return null;
            }
            current_interval = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
