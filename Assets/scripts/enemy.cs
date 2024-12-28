using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShootBullets());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootBullets()
    {
        Vector2 direction = Vector2.up;

        while (true)
        {
            GameObject new_bullet = Instantiate(bullet);
            new_bullet.transform.position = transform.position;
            new_bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;
            direction = Quaternion.AngleAxis(20, Vector3.forward) * direction;
            yield return new WaitForSeconds(1f);
        }
    }
}
