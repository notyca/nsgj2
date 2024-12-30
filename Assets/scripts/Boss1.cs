using System.Collections;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [SerializeField] private GameObject puffCloud;
    [SerializeField] private GameObject bullet;
    [SerializeField] private HitPoints hitPoints;
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
        StartCoroutine(ShootBullets());
    }

    IEnumerator ShootBullets()
    {
        Vector2 direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector2.up;

        float interval = 0.5f;

        float current_interval = 0;

        for(int round = 0; round < 5; ++round)
        {
            for (int i = 0; i < 12; ++i)
            {
                GameObject new_bullet = Instantiate(bullet);
                new_bullet.transform.position = transform.position;
                new_bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;
                new_bullet.GetComponent<bullet>().speed = bullet_speed;
                direction = Quaternion.AngleAxis(360 / 12, Vector3.forward) * direction;
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
                yield return null;
            }
            current_interval = 0;
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(ShootAtPlayer());
    }

    IEnumerator ShootAtPlayer()
    {
        Vector2 direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector2.up;

        float interval = 0.1f;

        float current_interval = 0;

        for (int round = 0; round < 50; ++round)
        {
            direction = (FindAnyObjectByType<PlayerMovement>().transform.position - transform.position).normalized;
            GameObject new_bullet = Instantiate(bullet);
            new_bullet.transform.position = transform.position;
            new_bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;
            new_bullet.GetComponent<bullet>().speed = bullet_speed * 2;
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
        yield return new WaitForSeconds(3f);
        StartCoroutine(ShootBullets());
    }

    IEnumerator Helix()
    {
        Vector2 direction = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector2.up;

        float interval = 0.1f;

        float current_interval = 0;

        float deviation = 0;

        for (int round = 0; round < 200; ++round)
        {
            for (int i = 0; i < 2; ++i)
            {
                direction = (FindAnyObjectByType<PlayerMovement>().transform.position - transform.position).normalized;
                if(i == 0)
                    direction = Quaternion.AngleAxis(deviation * 10, Vector3.forward) * direction;
                else
                    direction = Quaternion.AngleAxis(-deviation * 10, Vector3.forward) * direction;
                deviation = Mathf.Sin(Time.time * 4);
                GameObject new_bullet = Instantiate(bullet);
                new_bullet.transform.position = transform.position;
                new_bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;
                new_bullet.GetComponent<bullet>().speed = bullet_speed * 1;
            }
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
        yield return new WaitForSeconds(3f);
        StartCoroutine(ShootBullets());
    }

    private int hp = 8;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        --hp;
        hitPoints.ShowHitPoints(hp);
        if (hp > 0)
            return;
        Destroy(gameObject);
    }
}
